using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HeThongPOS.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        // This is only used for EF Core CLI migrations.
        // The actual connection string will come from AppSettings/Env during runtime.
        var connectionString = "Server=localhost,14335;Database=HeThongPOS;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;";
        
        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
