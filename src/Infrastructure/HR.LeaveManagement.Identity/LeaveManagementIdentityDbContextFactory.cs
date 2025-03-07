using HR.LeaveManagement.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class LeaveManagementIdentityDbContextFactory : IDesignTimeDbContextFactory<LeaveManagementIdentityDbContext>
{
    public LeaveManagementIdentityDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<LeaveManagementIdentityDbContext>();
            var connectionString = configuration.GetConnectionString("LeaveManagementIdentityConnectionString");

            builder.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(LeaveManagementIdentityDbContext).Assembly.FullName));

            return new LeaveManagementIdentityDbContext(builder.Options);
    }
}