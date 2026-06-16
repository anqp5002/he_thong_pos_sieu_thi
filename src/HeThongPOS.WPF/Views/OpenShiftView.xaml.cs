using System.Windows;
using System.Windows.Controls;
using HeThongPOS.WPF.ViewModels;

namespace HeThongPOS.WPF.Views;

public partial class OpenShiftView : Page
{
    public OpenShiftView()
    {
        InitializeComponent();
    }

    private void QuickAmount_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn && btn.Tag is string tagValue && DataContext is ShiftViewModel vm)
        {
            if (decimal.TryParse(tagValue, out decimal amount))
            {
                vm.SoDuDauCa = amount;
            }
        }
    }
}
