using System.Windows;
using HeThongPOS.WPF.ViewModels;

namespace HeThongPOS.WPF.Controls;

public partial class PaymentDialog : Window
{
    public PaymentDialog(PaymentViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;

        // Đăng ký event đóng cửa sổ
        viewModel.OnPaymentSuccess = () =>
        {
            DialogResult = true;
            Close();
        };

        viewModel.OnRequestClose = () =>
        {
            DialogResult = false;
            Close();
        };
    }
}
