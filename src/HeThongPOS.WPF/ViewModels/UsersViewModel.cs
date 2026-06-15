using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Core.Entities;
using HeThongPOS.Infrastructure.Data;
using HeThongPOS.WPF.Services;
using Microsoft.EntityFrameworkCore;

namespace HeThongPOS.WPF.ViewModels;

public partial class UsersViewModel : ObservableObject
{
    private readonly AppDbContext _context;

    [ObservableProperty]
    private ObservableCollection<NhanVien> _users = new();

    [ObservableProperty]
    private string _searchKeyword = string.Empty;

    [ObservableProperty]
    private NhanVien? _selectedUser;

    public UsersViewModel(AppDbContext context)
    {
        _context = context;
        LoadDataCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        try
        {
            var keyword = SearchKeyword.Trim().ToLower();

            var query = _context.NhanViens
                .Include(u => u.VaiTro)
                .AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(u => u.HoTen.ToLower().Contains(keyword) || 
                                         u.Username.ToLower().Contains(keyword) ||
                                         u.VaiTro.TenVaiTro.ToLower().Contains(keyword));
            }

            var list = await query.ToListAsync();

            Users.Clear();
            foreach (var user in list)
            {
                // Assign a fallback username if empty
                if (string.IsNullOrEmpty(user.Username))
                {
                    user.Username = user.Id == 1 ? "admin" : $"cashier{user.Id}";
                }
                Users.Add(user);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải danh sách nhân viên: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task ToggleUserStatusAsync()
    {
        if (SelectedUser == null)
        {
            MessageBox.Show("Vui lòng chọn một nhân viên từ danh sách.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var currentUser = SessionContext.CurrentUser;
        if (currentUser != null && SelectedUser.Id == currentUser.Id)
        {
            MessageBox.Show("Bạn không thể tự khóa tài khoản của chính mình!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        string actionText = SelectedUser.TrangThai ? "khóa" : "mở khóa";
        var result = MessageBox.Show($"Bạn có chắc chắn muốn {actionText} tài khoản của '{SelectedUser.HoTen}'?", 
            "Xác nhận thay đổi", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            try
            {
                var dbUser = await _context.NhanViens.FirstOrDefaultAsync(u => u.Id == SelectedUser.Id);
                if (dbUser != null)
                {
                    dbUser.TrangThai = !dbUser.TrangThai;
                    _context.NhanViens.Update(dbUser);
                    await _context.SaveChangesAsync();

                    MessageBox.Show($"Đã {actionText} tài khoản thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên trong cơ sở dữ liệu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật trạng thái: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
