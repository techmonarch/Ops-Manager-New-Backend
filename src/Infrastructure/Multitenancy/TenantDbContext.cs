using Finbuckle.MultiTenant.Stores;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Infrastructure.Persistence.Configuration;

namespace OpsManagerAPI.Infrastructure.Multitenancy;
public class TenantDbContext : EFCoreStoreDbContext<DiscoInfo>
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DiscoInfo>().ToTable("Tenants", SchemaNames.MultiTenancy);
    }
}