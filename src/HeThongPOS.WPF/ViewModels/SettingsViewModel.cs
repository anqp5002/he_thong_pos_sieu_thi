using System;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Core.Entities;
using HeThongPOS.Infrastructure.Data;
using HeThongPOS.WPF.Services;
using Microsoft.EntityFrameworkCore;

namespace HeThongPOS.WPF.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly AppDbContext _context;

    [ObservableProperty] private string _oldPassword = string.Empty;
    [ObservableProperty] private string _newPassword = string.Empty;
    [ObservableProperty] private string _confirmPassword = string.Empty;

    // Info
    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _fullName = string.Empty;
    [ObservableProperty] private string _roleName = string.Empty;

    // Mock configs
    [ObservableProperty] private string _storeName = "Siêu thị RetailPOS";
    [ObservableProperty] private string _storeAddress = "123 Đường Láng, Đống Đa, Hà Nội";
    [ObservableProperty] private string _taxCode = "0102030405";
    [ObservableProperty] private string _terminalId = "POS-01";

    public SettingsViewModel(AppDbContext context)
    {
        _context = context;
        LoadUserInfo();
    }

    private void LoadUserInfo()
    {
        var currentUser = SessionContext.CurrentUser;
        if (currentUser != null)
        {
            Username = string.IsNullOrEmpty(currentUser.Username) ? "admin" : currentUser.Username;
            FullName = currentUser.HoTen;
            RoleName = currentUser.VaiTro?.TenVaiTro ?? "Nhân viên";
        }
    }

    [RelayCommand]
    private async Task ChangePasswordAsync()
    {
        try
        {
            var currentUser = SessionContext.CurrentUser;
            if (currentUser == null)
            {
                MessageBox.Show("Không tìm thấy thông tin phiên làm việc hiện tại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(NewPassword) || NewPassword.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải từ 6 ký tự trở lên.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (NewPassword != ConfirmPassword)
            {
                MessageBox.Show("Xác nhận mật khẩu mới không khớp.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Verify old password. 
            // If the current hashed password in DB is empty, we allow any old password (or empty) to let user seed it.
            bool isOldPasswordValid = false;
            if (string.IsNullOrEmpty(currentUser.PasswordHash))
            {
                isOldPasswordValid = string.IsNullOrEmpty(OldPassword);
            }
            else
            {
                isOldPasswordValid = BCrypt.Net.BCrypt.Verify(OldPassword, currentUser.PasswordHash);
            }

            if (!isOldPasswordValid)
            {
                MessageBox.Show("Mật khẩu cũ không chính xác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Hash new password
            string newHash = BCrypt.Net.BCrypt.HashPassword(NewPassword);

            // Update DB
            var dbUser = await _context.NhanViens.FirstOrDefaultAsync(n => n.Id == currentUser.Id);
            if (dbUser != null)
            {
                dbUser.PasswordHash = newHash;
                _context.NhanViens.Update(dbUser);
                await _context.SaveChangesAsync();

                // Sync current session
                currentUser.PasswordHash = newHash;
                SessionContext.CurrentUser = currentUser;

                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                // Clear fields
                OldPassword = string.Empty;
                NewPassword = string.Empty;
                ConfirmPassword = string.Empty;
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên trong cơ sở dữ liệu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Đã xảy ra lỗi khi đổi mật khẩu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
