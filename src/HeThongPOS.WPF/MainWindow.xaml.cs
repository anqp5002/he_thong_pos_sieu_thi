using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using HeThongPOS.WPF.Services;
using HeThongPOS.WPF.ViewModels;

namespace HeThongPOS.WPF;

public partial class MainWindow : Window
{
    private INavigationService _navigationService = null!;

    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Get NavigationService from DI and set it up
        var app = (App)System.Windows.Application.Current;
        _navigationService = app.ServiceProvider.GetRequiredService<INavigationService>();
    }

    private void NavToPOS_Click(object sender, RoutedEventArgs e) => _navigationService.NavigateTo<POSViewModel>();
    private void NavToShift_Click(object sender, RoutedEventArgs e) => _navigationService.NavigateTo<ShiftViewModel>();
    private void NavToDashboard_Click(object sender, RoutedEventArgs e) => _navigationService.NavigateTo<DashboardViewModel>();
    private void NavToProducts_Click(object sender, RoutedEventArgs e) => _navigationService.NavigateTo<ProductsViewModel>();
    private void NavToCustomers_Click(object sender, RoutedEventArgs e) => _navigationService.NavigateTo<CustomersViewModel>();
    private void NavToLogin_Click(object sender, RoutedEventArgs e) => _navigationService.NavigateTo<LoginViewModel>();
}