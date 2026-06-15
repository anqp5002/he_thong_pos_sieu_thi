using System.Windows;
using System.Windows.Controls;
using HeThongPOS.WPF.ViewModels;

namespace HeThongPOS.WPF.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
    }

    private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
    {
        if (DataContext is SettingsViewModel vm)
        {
            vm.OldPassword = TxtOldPassword.Password;
            vm.NewPassword = TxtNewPassword.Password;
            vm.ConfirmPassword = TxtConfirmPassword.Password;

            vm.ChangePasswordCommand.Execute(null);

            // Clear the actual PasswordBox elements on success (since VM clears them on success)
            if (string.IsNullOrEmpty(vm.NewPassword))
            {
                TxtOldPassword.Clear();
                TxtNewPassword.Clear();
                TxtConfirmPassword.Clear();
            }
        }
    }

    private void BtnSaveConfig_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Đã lưu cấu hình hệ thống thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
