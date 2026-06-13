using System.Windows;

namespace HeThongPOS.WPF;

public partial class MainWindow : Window
{
    private HeThongPOS.WPF.Views.OrdersView _ordersView;
    private HeThongPOS.WPF.Views.CustomersView _customersView;

    public MainWindow(
        HeThongPOS.WPF.ViewModels.OrdersViewModel ordersViewModel,
        HeThongPOS.WPF.ViewModels.CustomersViewModel customersViewModel)
    {
        InitializeComponent();
        
        // Initialize views and assign ViewModels
        _ordersView = new HeThongPOS.WPF.Views.OrdersView { DataContext = ordersViewModel };
        _customersView = new HeThongPOS.WPF.Views.CustomersView { DataContext = customersViewModel };

        // Set initial view
        MainContent.Content = _ordersView;
        UpdateActiveButton(BtnOrders);
    }

    private void NavOrders_Click(object sender, RoutedEventArgs e)
    {
        MainContent.Content = _ordersView;
        UpdateActiveButton(BtnOrders);
    }

    private void NavCustomers_Click(object sender, RoutedEventArgs e)
    {
        MainContent.Content = _customersView;
        UpdateActiveButton(BtnCustomers);
    }

    public void NavigateToOrders()
    {
        MainContent.Content = _ordersView;
        UpdateActiveButton(BtnOrders);
    }

    private void UpdateActiveButton(System.Windows.Controls.Button activeBtn)
    {
        BtnOrders.Style = (Style)FindResource("OutlinedButton");
        BtnCustomers.Style = (Style)FindResource("OutlinedButton");
        
        activeBtn.Style = (Style)FindResource("PrimaryButton");
    }
}