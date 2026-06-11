using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using HeThongPOS.WPF.Views;
using HeThongPOS.WPF.ViewModels;

namespace HeThongPOS.WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        LoadPOSView();
    }

    private void Nav_POS_Click(object sender, RoutedEventArgs e)
    {
        LoadPOSView();
    }

    private void Nav_Reports_Click(object sender, RoutedEventArgs e)
    {
        var app = (App)System.Windows.Application.Current;
        var reportsViewModel = app.ServiceProvider.GetRequiredService<ReportsViewModel>();
        
        var reportsView = new ReportsView(reportsViewModel);
        
        MainFrame.Content = reportsView;
    }

    private void LoadPOSView()
    {
        var app = (App)System.Windows.Application.Current;
        var posViewModel = app.ServiceProvider.GetRequiredService<POSViewModel>();
        
        var posView = new POSView
        {
            DataContext = posViewModel
        };
        
        MainFrame.Content = posView;
    }
}