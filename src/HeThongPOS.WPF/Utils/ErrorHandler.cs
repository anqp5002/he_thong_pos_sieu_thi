using System;
using System.Windows;

namespace HeThongPOS.WPF.Utils;

public static class ErrorHandler
{
    /// <summary>
    /// Displays a standardized error dialog.
    /// </summary>
    public static void HandleError(Exception ex, string context = "")
    {
        string message = $"Đã xảy ra lỗi hệ thống: {ex.Message}";
        if (!string.IsNullOrEmpty(context))
        {
            message = $"Lỗi khi thực hiện '{context}':\n\n{ex.Message}";
        }

        MessageBox.Show(message, "Lỗi Hệ Thống", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    /// <summary>
    /// Displays a standardized warning dialog.
    /// </summary>
    public static void ShowWarning(string message, string title = "Cảnh Báo")
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    /// <summary>
    /// Displays a standardized informational dialog.
    /// </summary>
    public static void ShowInfo(string message, string title = "Thông Báo")
    {
        MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
