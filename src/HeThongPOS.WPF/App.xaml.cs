using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using HeThongPOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        var connectionString = "Server=(localdb)\\mssqllocaldb;Database=HeThongPOS;Trusted_Connection=True;TrustServerCertificate=true;";
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString), ServiceLifetime.Transient);

        // Repositories
        services.AddTransient<HeThongPOS.Core.Interfaces.ICustomerRepository, HeThongPOS.Infrastructure.Repositories.CustomerRepository>();
        services.AddTransient<HeThongPOS.Core.Interfaces.IOrderRepository, HeThongPOS.Infrastructure.Repositories.OrderRepository>();
        services.AddScoped<HeThongPOS.Core.Interfaces.IProductRepository, HeThongPOS.Infrastructure.Repositories.ProductRepository>();

        // Services
        services.AddTransient<HeThongPOS.Core.Interfaces.ICustomerService, HeThongPOS.Application.Services.CustomerService>();
        services.AddTransient<HeThongPOS.Core.Interfaces.IOrderService, HeThongPOS.Application.Services.OrderService>();
        services.AddTransient<HeThongPOS.Core.Interfaces.IInvoiceService, HeThongPOS.WPF.Services.InvoiceGenerator>();
        services.AddTransient<HeThongPOS.WPF.Services.PrintService>();
        services.AddScoped<HeThongPOS.Core.Interfaces.IProductService, HeThongPOS.Application.Services.ProductService>();

        // ViewModels
        services.AddTransient<HeThongPOS.WPF.ViewModels.CustomersViewModel>();
        services.AddTransient<HeThongPOS.WPF.ViewModels.OrdersViewModel>();
        services.AddTransient<HeThongPOS.WPF.ViewModels.BillPreviewViewModel>();
        services.AddTransient<HeThongPOS.WPF.ViewModels.ReportsViewModel>();
        services.AddTransient<HeThongPOS.WPF.ViewModels.SettingsViewModel>();
        services.AddTransient<HeThongPOS.WPF.ViewModels.UsersViewModel>();
        services.AddTransient<HeThongPOS.WPF.ViewModels.ProductsViewModel>();
        services.AddTransient<HeThongPOS.WPF.ViewModels.ShiftViewModel>();
        
        // Views
        services.AddTransient<MainWindow>();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        try
        {
            // Seed the current session user for testing
            using (var scope = ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Seed roles and cashier users if they don't exist in DB
                var cashierRole = dbContext.VaiTros.FirstOrDefault(r => r.TenVaiTro == "Thu ngân");
                if (cashierRole == null)
                {
                    cashierRole = new HeThongPOS.Core.Entities.VaiTro { TenVaiTro = "Thu ngân", MoTa = "Nhân viên thu ngân bán hàng" };
                    dbContext.VaiTros.Add(cashierRole);
                    dbContext.SaveChanges();
                }

                var cashier1 = dbContext.NhanViens.FirstOrDefault(n => n.Username == "cashier1");
                if (cashier1 == null)
                {
                    dbContext.NhanViens.Add(new HeThongPOS.Core.Entities.NhanVien 
                    { 
                        Username = "cashier1", 
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                        HoTen = "Nguyễn Thu Ngân", 
                        VaiTroId = cashierRole.Id, 
                        TrangThai = true 
                    });
                }

                var cashier2 = dbContext.NhanViens.FirstOrDefault(n => n.Username == "cashier2");
                if (cashier2 == null)
                {
                    dbContext.NhanViens.Add(new HeThongPOS.Core.Entities.NhanVien 
                    { 
                        Username = "cashier2", 
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                        HoTen = "Trần Văn Khóa", 
                        VaiTroId = cashierRole.Id, 
                        TrangThai = false 
                    });
                }

                // Ensure Admin user has password hashed as admin123
                var adminUser = dbContext.NhanViens.FirstOrDefault(n => n.Id == 1);
                if (adminUser != null)
                {
                    if (string.IsNullOrEmpty(adminUser.Username))
                    {
                        adminUser.Username = "admin";
                    }
                    if (string.IsNullOrEmpty(adminUser.PasswordHash))
                    {
                        adminUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123");
                    }
                    dbContext.NhanViens.Update(adminUser);
                }

                dbContext.SaveChanges();

                var employee = dbContext.NhanViens
                    .Include(n => n.VaiTro)
                    .FirstOrDefault(n => n.Id == 1);
                HeThongPOS.WPF.Services.SessionContext.CurrentUser = employee;
            }

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
