using System;
using System.Globalization;
using System.Windows.Data;

namespace HeThongPOS.WPF.Converters;

/// <summary>
/// Chuyển đổi số decimal thành chuỗi tiền tệ VNĐ. VD: 25000 -> "25.000 đ"
/// </summary>
public class CurrencyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal amount)
        {
            return amount.ToString("#,##0") + " đ";
        }
        return "0 đ";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
