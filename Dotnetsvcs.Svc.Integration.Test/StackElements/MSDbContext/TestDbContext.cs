using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.MSDbContext;

public class TestDbContext : DbContext {
    public TestDbContext(DbContextOptions options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        var modelConfigurationAssembly = Assembly.GetAssembly(typeof(TestDbContext))!;
        modelBuilder.ApplyConfigurationsFromAssembly(modelConfigurationAssembly);
    }
}
