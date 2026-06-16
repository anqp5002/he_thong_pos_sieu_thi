using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using HeThongPOS.WPF.Services;
using HeThongPOS.WPF.ViewModels;

namespace HeThongPOS.WPF;

public partial class MainWindow : Window
{
    private INavigationService _navigationService = null!;
    private SessionManager _sessionManager = null!;
    private StockAlertService _stockAlertService = null!;

    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Được gọi từ App.xaml.cs sau khi khởi tạo DI xong
    /// </summary>
    public void InitializeServices(INavigationService navService, SessionManager session, StockAlertService stockAlert)
    {
        _navigationService = navService;
        _sessionManager = session;
        _stockAlertService = stockAlert;

        // Khởi tạo NavigationService với LoginFrame (dùng lúc chưa login)
        var nav = (NavigationService)_navigationService;
        nav.Initialize(LoginFrame);

        // Lắng nghe sự kiện session thay đổi
        _sessionManager.OnSessionChanged += OnSessionChanged;

        // Bắt đầu ở màn hình Login
        _navigationService.NavigateTo<LoginViewModel>();
    }

    /// <summary>
    /// Xử lý khi session thay đổi (đăng nhập hoặc đăng xuất)
    /// </summary>
    private async void OnSessionChanged()
    {
        if (_sessionManager.IsLoggedIn)
        {
            // === ĐĂNG NHẬP THÀNH CÔNG ===
            // 1. Chuyển NavigationService sang MainFrame
            var nav = (NavigationService)_navigationService;
            nav.Initialize(MainFrame);

            // 2. Hiển thị Layout Sidebar, ẩn LoginFrame
            LoginFrame.Visibility = Visibility.Collapsed;
            LayoutWithSidebar.Visibility = Visibility.Visible;

            // 3. Cập nhật thông tin user trên UI
            UpdateUserInfo();

            // 4. Áp dụng phân quyền (Role-based Access Control)
            ApplyRoleBasedAccess();

            // 5. Kiểm tra Stock Alert
            await CheckStockAlert();

            // 6. Điều hướng đến POS
            _navigationService.NavigateTo<POSViewModel>();
            PageTitle.Text = "Bán Hàng";
        }
        else
        {
            // === ĐĂNG XUẤT ===
            // 1. Chuyển NavigationService về LoginFrame
            var nav = (NavigationService)_navigationService;
            nav.Initialize(LoginFrame);

            // 2. Ẩn Layout Sidebar, hiện LoginFrame
            LayoutWithSidebar.Visibility = Visibility.Collapsed;
            LoginFrame.Visibility = Visibility.Visible;
            StockAlertBar.Visibility = Visibility.Collapsed;

            // 3. Điều hướng về Login
            _navigationService.NavigateTo<LoginViewModel>();
        }
    }

    /// <summary>
    /// Cập nhật thông tin nhân viên lên Sidebar và Header
    /// </summary>
    private void UpdateUserInfo()
    {
        var user = _sessionManager.CurrentUser;
        if (user == null) return;

        // Sidebar User Info
        UserNameText.Text = user.HoTen;
        UserRoleText.Text = user.VaiTro?.TenVaiTro ?? "Nhân viên";
        UserInitialsText.Text = _sessionManager.UserInitials;

        // Header User Info
        HeaderUserName.Text = user.HoTen;
        HeaderUserInitials.Text = _sessionManager.UserInitials;
    }

    /// <summary>
    /// Mục 6: Phân quyền hiển thị menu theo Role
    /// Admin thấy hết, Cashier chỉ thấy POS và Ca làm việc
    /// </summary>
    private void ApplyRoleBasedAccess()
    {
        bool isAdmin = _sessionManager.IsAdmin;

        // Menu quản lý chỉ Admin mới thấy
        LabelQuanLy.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
        BtnProducts.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
        BtnCustomers.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
        BtnDashboard.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;

        // POS và Ca Làm Việc luôn hiển thị
        BtnPOS.Visibility = Visibility.Visible;
        BtnShift.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Mục 7: Kiểm tra tồn kho thấp và hiển thị cảnh báo
    /// </summary>
    private async System.Threading.Tasks.Task CheckStockAlert()
    {
        try
        {
            int lowStockCount = await _stockAlertService.CountLowStockAsync();
            if (lowStockCount > 0)
            {
                StockAlertText.Text = $"Cảnh báo: Có {lowStockCount} sản phẩm sắp hết hàng (tồn kho ≤ 10). Vui lòng kiểm tra và bổ sung!";
                StockAlertBar.Visibility = Visibility.Visible;
            }
            else
            {
                StockAlertBar.Visibility = Visibility.Collapsed;
            }
        }
        catch
        {
            // Bỏ qua lỗi stock alert để không ảnh hưởng luồng chính
            StockAlertBar.Visibility = Visibility.Collapsed;
        }
    }

    // ===== NAVIGATION EVENT HANDLERS =====

    private void NavToPOS_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo<POSViewModel>();
        PageTitle.Text = "Bán Hàng";
    }

    private void NavToShift_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo<ShiftViewModel>();
        PageTitle.Text = "Quản Lý Ca Làm Việc";
    }

    private void NavToDashboard_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo<DashboardViewModel>();
        PageTitle.Text = "Thống Kê / Báo Cáo";
    }

    private void NavToProducts_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo<ProductsViewModel>();
        PageTitle.Text = "Quản Lý Sản Phẩm";
    }

    private void NavToCustomers_Click(object sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo<CustomersViewModel>();
        PageTitle.Text = "Quản Lý Khách Hàng";
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            _sessionManager.Logout();
        }
    }

    private void CloseStockAlert_Click(object sender, RoutedEventArgs e)
    {
        StockAlertBar.Visibility = Visibility.Collapsed;
    }
}