using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

namespace OpsManagerAPI.Infrastructure.Persistence.Configuration;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder) =>
        builder
            .ToTable("Customers", SchemaNames.Dbo)
            .IsMultiTenant();
}

public class DistributionTransformerConfig : IEntityTypeConfiguration<DistributionTransformer>
{
    public void Configure(EntityTypeBuilder<DistributionTransformer> builder) =>
        builder
            .ToTable("DistributionTransformers", SchemaNames.Dbo)
            .IsMultiTenant();
}

public class BillingConfig : IEntityTypeConfiguration<Billing>
{
    public void Configure(EntityTypeBuilder<Billing> builder) =>
        builder
            .ToTable("Billings", SchemaNames.Dbo)
            .IsMultiTenant();
}

public class BillDistributionConfig : IEntityTypeConfiguration<BillDistribution>
{
    public void Configure(EntityTypeBuilder<BillDistribution> builder) =>
        builder
            .ToTable("BillDistributions", SchemaNames.Dbo)
            .IsMultiTenant();
}

public class TariffConfig : IEntityTypeConfiguration<Tariff>
{
    public void Configure(EntityTypeBuilder<Tariff> builder) =>
        builder
            .ToTable("Tariffs", SchemaNames.Dbo)
            .IsMultiTenant();
}

public class DisconnectionConfig : IEntityTypeConfiguration<Disconnection>
{
    public void Configure(EntityTypeBuilder<Disconnection> builder) =>
        builder
            .ToTable("Disconnections", SchemaNames.Dbo)
            .IsMultiTenant();
}

public class DisconnectionReasonConfig : IEntityTypeConfiguration<DisconnectionReason>
{
    public void Configure(EntityTypeBuilder<DisconnectionReason> builder) =>
        builder
            .ToTable("DisconnectionsReasons", SchemaNames.Dbo)
            .IsMultiTenant();
}

public class ReconnectionConfig : IEntityTypeConfiguration<Reconnection>
{
    public void Configure(EntityTypeBuilder<Reconnection> builder) =>
        builder
            .ToTable("Reconnections", SchemaNames.Dbo)
            .IsMultiTenant();
}

public class ComplaintConfig : IEntityTypeConfiguration<Complaint>
{
    public void Configure(EntityTypeBuilder<Complaint> builder) =>
        builder
            .ToTable("Complaints", SchemaNames.Dbo)
            .IsMultiTenant();
}

public class MeterReadingConfig : IEntityTypeConfiguration<MeterReading>
{
    public void Configure(EntityTypeBuilder<MeterReading> builder) =>
        builder
            .ToTable("MeterReadings", SchemaNames.Dbo)
            .IsMultiTenant();
}

public class OfficeConfig : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder
            .ToTable("Offices", SchemaNames.Dbo)
            .IsMultiTenant();

        builder
           .HasMany(o => o.Staffs)
           .WithOne(st => st.Office)
           .HasForeignKey(st => st.OfficeId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}

public class OfficeLevelConfig : IEntityTypeConfiguration<OfficeLevel>
{
    public void Configure(EntityTypeBuilder<OfficeLevel> builder) =>
        builder
            .ToTable("OfficeLevels", SchemaNames.Dbo)
            .IsMultiTenant();
}

public class EnumerationConfig : IEntityTypeConfiguration<Enumeration>
{
    public void Configure(EntityTypeBuilder<Enumeration> builder)
    {
        builder
            .ToTable("Enumerations", SchemaNames.Dbo).
            IsMultiTenant();

        builder.Property(e => e.ImagesUrl)
                    .HasConversion(
                        v => string.Join(";", v),
                        v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList());
    }
}

public class DownloadManagerConfig : IEntityTypeConfiguration<DownloadManager>
{
    public void Configure(EntityTypeBuilder<DownloadManager> builder)
    {
        builder
            .ToTable("DownloadManager", SchemaNames.Dbo).
            IsMultiTenant();

        builder.Property(e => e.Roles)
                    .HasConversion(
                        v => string.Join(";", v),
                        v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList());
    }
}

public class EvaluationConfig : IEntityTypeConfiguration<Evaluation>
{
    public void Configure(EntityTypeBuilder<Evaluation> builder)
    {
        builder
            .ToTable("Evaluations", SchemaNames.Dbo)
            .IsMultiTenant();
    }
}

public class StaffConfig : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder
            .ToTable("Staffs", SchemaNames.Dbo)
            .IsMultiTenant();
    }
}

public class StaffTeamConfig : IEntityTypeConfiguration<StaffTeam>
{
    public void Configure(EntityTypeBuilder<StaffTeam> builder)
    {
        builder
            .ToTable("StaffTeams", SchemaNames.Dbo)
            .IsMultiTenant();
    }
}

public class PaymentConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder) =>
        builder
            .ToTable("Payments", SchemaNames.Dbo)
            .IsMultiTenant();
}