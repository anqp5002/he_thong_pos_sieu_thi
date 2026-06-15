using System.Windows;
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
        // Database
        var connectionString = "Server=localhost,14335;Database=HeThongPOS;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;";
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Repositories
        services.AddScoped<HeThongPOS.Core.Interfaces.IProductRepository, HeThongPOS.Infrastructure.Repositories.ProductRepository>();

        // Services
        services.AddScoped<HeThongPOS.Core.Interfaces.IProductService, HeThongPOS.Application.Services.ProductService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IShiftService, ShiftService>();

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
        services.AddTransient<HeThongPOS.WPF.Controls.PaymentDialog>();
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

            // 2. Initialize MainWindow and NavigationService
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            var navService = (NavigationService)ServiceProvider.GetRequiredService<INavigationService>();
            navService.Initialize(mainWindow.MainFrame);

            mainWindow.Show();

            // 3. Navigate to LoginView
            navService.NavigateTo<LoginViewModel>();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi khởi động: {ex.Message}\n{ex.InnerException?.Message}", "Lỗi khởi động", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }
    }
}
