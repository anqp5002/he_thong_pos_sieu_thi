using System;
using System.Globalization;
using System.Windows.Data;

namespace HeThongPOS.WPF.Converters;

public class BooleanToLoginTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isBusy && isBusy)
        {
            return "Đang đăng nhập...";
        }
        return "Đăng Nhập";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
