using System;
using System.Globalization;
using System.Windows.Data;

namespace HeThongPOS.WPF.Converters;

public class ImageUrlConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string? url = value as string;
        if (string.IsNullOrWhiteSpace(url))
        {
            // Trả về ảnh mặc định nếu URL rỗng
            return "https://placehold.co/150x150?text=No+Image";
        }
        
        // Nếu là URL hợp lệ, trả về URL đó
        return url;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
