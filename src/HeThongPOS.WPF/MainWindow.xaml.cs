using System.Windows;
using HeThongPOS.WPF.Services;
using HeThongPOS.WPF.ViewModels;
using HeThongPOS.WPF.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HeThongPOS.WPF;

public partial class MainWindow : Window
{
    private readonly OrdersView _ordersView;
    private readonly CustomersView _customersView;
    private readonly ReportsView _reportsView;
    private readonly UsersView _usersView;
    private readonly SettingsView _settingsView;
    private readonly ProductsView _productsView;
    private readonly ShiftView _shiftView;

    public MainWindow(
        OrdersViewModel ordersViewModel,
        CustomersViewModel customersViewModel,
        ReportsViewModel reportsViewModel,
        UsersViewModel usersViewModel,
        SettingsViewModel settingsViewModel,
        ProductsViewModel productsViewModel,
        ShiftViewModel shiftViewModel)
    {
        InitializeComponent();
        
        // Initialize views and assign ViewModels
        _ordersView = new OrdersView { DataContext = ordersViewModel };
        _customersView = new CustomersView { DataContext = customersViewModel };
        _reportsView = new ReportsView { DataContext = reportsViewModel };
        _usersView = new UsersView { DataContext = usersViewModel };
        _settingsView = new SettingsView { DataContext = settingsViewModel };
        _productsView = new ProductsView { DataContext = productsViewModel };
        _shiftView = new ShiftView { DataContext = shiftViewModel };

        // Set initial view
        MainContent.Content = _ordersView;
        UpdateActiveButton(BtnOrders);

        // Load active session user profile
        LoadUserProfile();
    }

    private void LoadUserProfile()
    {
        var currentUser = SessionContext.CurrentUser;
        if (currentUser != null)
        {
            TxtProfileName.Text = currentUser.HoTen;
            TxtProfileRole.Text = currentUser.VaiTro?.TenVaiTro ?? "Nhân viên";
            if (!string.IsNullOrEmpty(currentUser.HoTen))
            {
                TxtProfileInitials.Text = currentUser.HoTen[0].ToString().ToUpper();
            }

            // Role check: Only manager (Quản lý) can view the cashier account locking tab
            if (currentUser.VaiTro?.TenVaiTro != "Quản lý")
            {
                BtnUsers.Visibility = Visibility.Collapsed;
            }
        }
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

    private void NavReports_Click(object sender, RoutedEventArgs e)
    {
        MainContent.Content = _reportsView;
        UpdateActiveButton(BtnReports);
        
        // Refresh reports when clicked
        if (_reportsView.DataContext is ReportsViewModel vm)
        {
            vm.LoadDataCommand.Execute(null);
        }
    }

    private void NavUsers_Click(object sender, RoutedEventArgs e)
    {
        MainContent.Content = _usersView;
        UpdateActiveButton(BtnUsers);

        // Refresh users when clicked
        if (_usersView.DataContext is UsersViewModel vm)
        {
            vm.LoadDataCommand.Execute(null);
        }
    }

    private void NavSettings_Click(object sender, RoutedEventArgs e)
    {
        MainContent.Content = _settingsView;
        UpdateActiveButton(BtnSettings);
    }

    private void NavProducts_Click(object sender, RoutedEventArgs e)
    {
        MainContent.Content = _productsView;
        UpdateActiveButton(BtnProducts);
    }

    private void NavShift_Click(object sender, RoutedEventArgs e)
    {
        MainContent.Content = _shiftView;
        UpdateActiveButton(BtnShift);

        // Refresh shift stats when navigated
        if (_shiftView.DataContext is ShiftViewModel vm)
        {
            vm.LoadShiftDataCommand.Execute(null);
        }
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
        BtnReports.Style = (Style)FindResource("OutlinedButton");
        BtnProducts.Style = (Style)FindResource("OutlinedButton");
        BtnShift.Style = (Style)FindResource("OutlinedButton");
        BtnUsers.Style = (Style)FindResource("OutlinedButton");
        BtnSettings.Style = (Style)FindResource("OutlinedButton");
        
        activeBtn.Style = (Style)FindResource("PrimaryButton");
    }
}