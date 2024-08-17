using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpsManagerAPI.Application.Common.Interfaces;
using OpsManagerAPI.Domain.Aggregates.BillingAggregate;
using OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Aggregates.DownloadsManager;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using OpsManagerAPI.Domain.Aggregates.OfficeAggregate;
using OpsManagerAPI.Domain.Aggregates.PaymentAggregate;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;
using OpsManagerAPI.Domain.Aggregates.TariffsAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Configuration;

namespace OpsManagerAPI.Infrastructure.Persistence.Context;
public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings)
        : base(currentTenant, options, currentUser, serializer, dbSettings)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<DistributionTransformer> DistributionTransformers => Set<DistributionTransformer>();
    public DbSet<Billing> Billings => Set<Billing>();
    public DbSet<BillDistribution> BillDistributions => Set<BillDistribution>();
    public DbSet<Tariff> Tariffs => Set<Tariff>();
    public DbSet<Disconnection> Disconnections => Set<Disconnection>();
    public DbSet<Reconnection> Reconnections => Set<Reconnection>();
    public DbSet<Complaint> Complaints => Set<Complaint>();
    public DbSet<MeterReading> MeterReadings => Set<MeterReading>();
    public DbSet<Office> Offices => Set<Office>();
    public DbSet<OfficeLevel> OfficeLevels => Set<OfficeLevel>();
    public DbSet<Enumeration> Enumerations => Set<Enumeration>();
    public DbSet<Evaluation> Evaluations => Set<Evaluation>();
    public DbSet<Staff> Staffs => Set<Staff>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<StaffTeam> StaffTeams => Set<StaffTeam>();
    public DbSet<DisconnectionReason> DisconnectionReasons => Set<DisconnectionReason>();
    public DbSet<ComplaintSubCategory> ComplaintSubCategories => Set<ComplaintSubCategory>();
    public DbSet<ComplaintCategory> ComplaintCategories => Set<ComplaintCategory>();
    public DbSet<DownloadManager> DownloadManagers => Set<DownloadManager>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaNames.Dbo);
    }
}