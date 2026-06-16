using System.Windows;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using HeThongPOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using HeThongPOS.Application.Interfaces;
using HeThongPOS.Application.Services;
using HeThongPOS.WPF.Services;
using HeThongPOS.WPF.ViewModels;
using HeThongPOS.WPF.Views;

namespace HeThongPOS.WPF;

public partial class App : System.Windows.Application
{
    public IServiceProvider ServiceProvider { get; private set; }

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Mục 4: Đọc connection string từ appsettings.json thay vì hardcode
        string connectionString = GetConnectionString();
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString)
                   .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));

        // Repositories
        services.AddScoped<HeThongPOS.Core.Interfaces.IProductRepository, HeThongPOS.Infrastructure.Repositories.ProductRepository>();
        services.AddScoped<HeThongPOS.Core.Interfaces.IOrderRepository, HeThongPOS.Infrastructure.Repositories.OrderRepository>();

        // Services
        services.AddScoped<HeThongPOS.Core.Interfaces.IProductService, HeThongPOS.Application.Services.ProductService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IShiftService, ShiftService>();
        services.AddScoped<HeThongPOS.Core.Interfaces.IOrderService, HeThongPOS.Application.Services.OrderService>();

        // Mục 6: SessionManager (Singleton) - Global Auth State
        services.AddSingleton<SessionManager>();

        // Mục 7: StockAlertService
        services.AddScoped<StockAlertService>();

        // ViewModels
        services.AddTransient<HeThongPOS.WPF.ViewModels.ProductsViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<POSViewModel>();
        services.AddTransient<PaymentViewModel>();
        services.AddTransient<DashboardViewModel>();
        services.AddTransient<ShiftViewModel>();

        // Views
        services.AddTransient<MainWindow>();
        services.AddTransient<LoginView>();
        services.AddTransient<POSView>();
        services.AddTransient<HeThongPOS.WPF.Views.ShiftView>();
        services.AddTransient<HeThongPOS.WPF.Views.DashboardView>();
        services.AddTransient<HeThongPOS.WPF.Views.ProductsView>();
        services.AddTransient<HeThongPOS.WPF.Views.CustomersView>();
        services.AddTransient<HeThongPOS.WPF.Controls.PaymentDialog>();
    }

    /// <summary>
    /// Mục 4: Đọc connection string từ appsettings.json
    /// </summary>
    private string GetConnectionString()
    {
        string defaultConn = "Server=localhost,14335;Database=HeThongPOS;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;";
        try
        {
            string appSettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            if (File.Exists(appSettingsPath))
            {
                string json = File.ReadAllText(appSettingsPath);
                using var doc = JsonDocument.Parse(json);
                var connStrings = doc.RootElement.GetProperty("ConnectionStrings");
                return connStrings.GetProperty("DefaultConnection").GetString() ?? defaultConn;
            }
        }
        catch
        {
            // Nếu đọc file lỗi, dùng connection string mặc định
        }
        return defaultConn;
    }

    private async void OnStartup(object sender, StartupEventArgs e)
    {
        try
        {
            // 1. Run DataSeeder
            using (var scope = ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await DataSeeder.SeedAsync(dbContext);
            }

            // 2. Khởi tạo MainWindow
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

            // 3. Inject services vào MainWindow
            var navService = ServiceProvider.GetRequiredService<INavigationService>();
            var sessionManager = ServiceProvider.GetRequiredService<SessionManager>();

            // Tạo scope cho StockAlertService
            using var alertScope = ServiceProvider.CreateScope();
            var stockAlertService = alertScope.ServiceProvider.GetRequiredService<StockAlertService>();

            mainWindow.InitializeServices(navService, sessionManager, stockAlertService);
            mainWindow.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi khởi động: {ex.Message}\n{ex.InnerException?.Message}", "Lỗi khởi động", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }
    }
}
