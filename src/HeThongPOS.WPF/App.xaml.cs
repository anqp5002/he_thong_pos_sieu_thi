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
        var connectionString = "Server=localhost,1433;Database=HeThongPOS;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;";
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Services
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddScoped<IAuthService, AuthService>();

        // ViewModels
        services.AddTransient<LoginViewModel>();
        services.AddTransient<POSViewModel>();
        services.AddTransient<PaymentViewModel>();

        // Views
        services.AddTransient<MainWindow>();
        services.AddTransient<LoginView>();
        services.AddTransient<POSView>();
        services.AddTransient<HeThongPOS.WPF.Controls.PaymentDialog>();
    }

    private async void OnStartup(object sender, StartupEventArgs e)
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
}
