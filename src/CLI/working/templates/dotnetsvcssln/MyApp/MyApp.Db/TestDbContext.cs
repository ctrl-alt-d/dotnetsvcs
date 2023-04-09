using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MyApp.Db;

public class TestDbContext : IdentityDbContext
{
    public TestDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var modelConfigurationAssembly = Assembly.GetAssembly(typeof(TestDbContext))!;
        modelBuilder.ApplyConfigurationsFromAssembly(modelConfigurationAssembly);
    }
}
