using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Common;

namespace OpsManagerAPI.Domain.Aggregates.TariffsAggregate;

public class Tariff : AuditableEntity, IAggregateRoot
{
    public string UniqueId { get; private set; } = default!;
    public string TariffCode { get; private set; } = default!;
    public int MinimumHours { get; private set; }
    public string TariffRate { get; private set; } = default!;
    public decimal Amount { get; private set; }

    public string? ServiceBandName { get; internal set; }

    #region Relationships
    public ICollection<Customer> Customers { get; private set; } = new HashSet<Customer>();
    public ICollection<Enumeration> Enumerations { get; private set; } = new HashSet<Enumeration>();
    #endregion

    #region Constructors
    public Tariff(string tariffCode, string tariffRate, decimal amount, string serviceBandName, int minimumHours)
    {
        TariffCode = tariffCode;
        TariffRate = tariffRate;
        Amount = amount;
        ServiceBandName = serviceBandName;
        MinimumHours = minimumHours;
        UniqueId = $"{tariffCode}-{serviceBandName}";
    }

    // Parameterless constructor for ORM
    private Tariff()
    {
    }

    #endregion
}