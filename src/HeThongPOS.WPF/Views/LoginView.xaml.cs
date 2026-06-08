using System.Windows;
using System.Windows.Controls;
using HeThongPOS.WPF.ViewModels;

namespace HeThongPOS.WPF.Views;

public partial class LoginView : Page
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel viewModel)
        {
            viewModel.Password = PasswordBox.Password;
        }
    }
}
