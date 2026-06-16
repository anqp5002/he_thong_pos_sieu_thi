using System.Windows;
using System.Windows.Controls;

namespace HeThongPOS.WPF.Controls;

public partial class ReceiptDialog : Window
{
    public ReceiptDialog()
    {
        InitializeComponent();
    }

    private void PrintReceipt_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(ReceiptContent, "Hóa đơn POS");
                MessageBox.Show("In hóa đơn thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (System.Exception ex)
        {
            MessageBox.Show($"Lỗi khi in: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
