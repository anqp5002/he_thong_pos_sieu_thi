using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace HeThongPOS.WPF.Converters;

/// <summary>
/// Converter tự động tạo số thứ tự (STT) cho các dòng trong DataGrid.
/// Nhận DataGridRow và trả về index + 1.
/// </summary>
public class RowIndexConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DataGridRow row)
        {
            return row.GetIndex() + 1;
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
