using System.Windows;
using HeThongPOS.WPF.ViewModels;

namespace HeThongPOS.WPF.Controls;

public partial class PaymentDialog : Window
{
    public PaymentDialog()
    {
        InitializeComponent();
        
        Loaded += (s, e) => 
        {
            if (DataContext is PaymentViewModel vm)
            {
                vm.OnPaymentSuccess = () => 
                {
                    DialogResult = true;
                    Close();
                };
                vm.OnCancel = () => 
                {
                    DialogResult = false;
                    Close();
                };
            }
        };
    }
}
