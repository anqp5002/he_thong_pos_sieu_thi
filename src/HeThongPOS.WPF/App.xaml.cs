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
        var connectionString = "Server=localhost,1433;Database=HeThongPOS;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;";
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString), ServiceLifetime.Transient);

        // Repositories
        services.AddTransient<HeThongPOS.Core.Interfaces.ICustomerRepository, HeThongPOS.Infrastructure.Repositories.CustomerRepository>();
        services.AddTransient<HeThongPOS.Core.Interfaces.IOrderRepository, HeThongPOS.Infrastructure.Repositories.OrderRepository>();

        // Services
        services.AddTransient<HeThongPOS.Core.Interfaces.ICustomerService, HeThongPOS.Application.Services.CustomerService>();
        services.AddTransient<HeThongPOS.Core.Interfaces.IOrderService, HeThongPOS.Application.Services.OrderService>();

        // ViewModels
        services.AddTransient<HeThongPOS.WPF.ViewModels.CustomersViewModel>();
        services.AddTransient<HeThongPOS.WPF.ViewModels.OrdersViewModel>();
        
        // Views
        services.AddTransient<MainWindow>();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
