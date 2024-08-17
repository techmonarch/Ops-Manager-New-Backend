using OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Aggregates.OfficeAggregate;
using OpsManagerAPI.Domain.Common;

namespace OpsManagerAPI.Domain.Aggregates.MeterAggregate;
public class DistributionTransformer : AuditableEntity, IAggregateRoot
{
    public DefaultIdType OfficeId { get; private set; }
    public string Name { get; private set; } = default!;
    public decimal Longitude { get; private set; }
    public decimal Latitude { get; private set; }
    public DateTime InstallationDate { get; private set; }
    public int Capacity { get; private set; }
    public string Status { get; private set; } = default!;
    public int Rating { get; private set; }
    public string Maker { get; private set; } = default!;
    public string FeederPillarType { get; private set; } = default!;

    #region Relationships

    public Office? Store { get; private set; }
    public ICollection<Customer> Customers { get; private set; } = new HashSet<Customer>();
    public ICollection<MeterReading> MeterReadings { get; private set; } = new HashSet<MeterReading>();
    public ICollection<Complaint> Complaints { get; private set; } = new HashSet<Complaint>();
    public ICollection<Enumeration> Enumerations { get; private set; } = new HashSet<Enumeration>();
    #endregion

    #region Constructors
    private DistributionTransformer()
    {
    }

    public DistributionTransformer(DefaultIdType officeId, string name, decimal longitude, decimal latitude, DateTime installationDate, int capacity, string status, int rating, string maker, string feederPillarType)
    {
        OfficeId = officeId;
        Name = name;
        Longitude = longitude;
        Latitude = latitude;
        InstallationDate = installationDate;
        Capacity = capacity;
        Status = status;
        Rating = rating;
        Maker = maker;
        FeederPillarType = feederPillarType;
    }
    #endregion
}
