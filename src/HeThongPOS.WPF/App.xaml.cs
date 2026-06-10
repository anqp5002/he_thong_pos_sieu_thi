using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using HeThongPOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

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

        // ViewModels
        services.AddTransient<HeThongPOS.WPF.ViewModels.ProductsViewModel>();

        // Views
        services.AddTransient<MainWindow>();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        try
        {
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi khởi động: {ex.Message}\n{ex.InnerException?.Message}", "Lỗi khởi động", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }
    }
}
