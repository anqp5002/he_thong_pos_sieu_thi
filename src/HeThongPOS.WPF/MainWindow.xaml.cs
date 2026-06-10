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
        
        // Load ProductsView to test Sprint 1-Dev B
        var app = (App)System.Windows.Application.Current;
        var productsViewModel = app.ServiceProvider.GetRequiredService<ProductsViewModel>();
        
        var productsView = new ProductsView
        {
            DataContext = productsViewModel
        };
        
        MainFrame.Content = productsView;
    }
}