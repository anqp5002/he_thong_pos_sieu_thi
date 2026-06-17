using System.Windows.Controls;

namespace HeThongPOS.WPF.Views;

public partial class POSView : Page
{
    public POSView()
    {
        InitializeComponent();
        this.Loaded += (s, e) => 
        {
            BarcodeTextBox.Focus();
        };

        // Tự động bắt focus lại vào ô mã vạch nếu có dữ liệu nhập từ bàn phím/máy quét
        this.PreviewTextInput += (s, e) =>
        {
            if (!BarcodeTextBox.IsFocused)
            {
                BarcodeTextBox.Focus();
                BarcodeTextBox.Text += e.Text;
                BarcodeTextBox.CaretIndex = BarcodeTextBox.Text.Length;
                e.Handled = true;
            }
        };
    }
}
