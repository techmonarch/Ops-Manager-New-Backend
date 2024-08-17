using OpsManagerAPI.Domain.Aggregates.BillingAggregate;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using OpsManagerAPI.Domain.Aggregates.PaymentAggregate;
using OpsManagerAPI.Domain.Aggregates.TariffsAggregate;
using OpsManagerAPI.Domain.Common;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
public class Customer : AuditableEntity, IAggregateRoot
{
    public string AccountNumber { get; private set; } = default!;
    public string? MeterNumber { get; private set; }
    public DefaultIdType TariffId { get; private set; }
    public DefaultIdType DistributionTransformerId { get; private set; }
    public string FirstName { get; private set; } = default!;
    public string MiddleName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Phone { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string LGA { get; private set; } = default!;
    public decimal Longitude { get; private set; }
    public decimal Latitude { get; private set; }
    public CustomerType CustomerType { get; private set; }
    public CustomerStatus Status { get; private set; }
    public AccountType AccountType { get; private set; }

    #region Relationships
    public Tariff? Tariff { get; private set; }
    public DistributionTransformer? DistributionTransformer { get; private set; }
    public ICollection<Enumeration> Enumerations { get; private set; } = new HashSet<Enumeration>();
    public ICollection<Billing> Billings { get; private set; } = new HashSet<Billing>();
    public ICollection<MeterReading> MeterReadings { get; private set; } = new HashSet<MeterReading>();
    public ICollection<BillDistribution> BillDistributions { get; private set; } = new HashSet<BillDistribution>();
    public ICollection<Evaluation> Evaluations { get; private set; } = new HashSet<Evaluation>();
    public ICollection<Disconnection> Disconnections { get; private set; } = new HashSet<Disconnection>();
    public ICollection<Reconnection> Reconnections { get; private set; } = new HashSet<Reconnection>();
    public ICollection<Payment> Payments { get; private set; } = new HashSet<Payment>();
    #endregion

    #region Constructors
    private Customer()
    {
    }

    public Customer(string accountNumber, string meterNumber, string firstName, string middleName, string lastName, string phone, string email, string address, string city, string state, string lGA, decimal longitude, decimal latitude, CustomerType customerType, AccountType accountType, DefaultIdType tariffId, DefaultIdType distributionTransformerId)
    {
        AccountNumber = accountNumber;
        MeterNumber = meterNumber;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Address = address;
        City = city;
        State = state;
        LGA = lGA;
        Longitude = longitude;
        Latitude = latitude;
        CustomerType = customerType;
        AccountType = accountType;
        TariffId = tariffId;
        DistributionTransformerId = distributionTransformerId;
        Status = CustomerStatus.Active;
    }
    #endregion
}
