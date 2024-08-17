using OpsManagerAPI.Domain.Common;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Aggregates.BillingAggregate;
using OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using OpsManagerAPI.Domain.Aggregates.OfficeAggregate;

namespace OpsManagerAPI.Domain.Aggregates.StaffAggregate;

public class Staff : AuditableEntity, IAggregateRoot
{
    public DefaultIdType OfficeId { get; private set; }
    public DefaultIdType ApplicationUserId { get; private set; }
    public string StaffNumber { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string LGA { get; private set; } = default!;
    public bool IsSuperAdmin { get; private set; }

    #region Relationships
    public ICollection<StaffTeam> StaffTeams { get; private set; } = new HashSet<StaffTeam>();
    public ICollection<MeterReading> MeterReadings { get; private set; } = new HashSet<MeterReading>();
    public ICollection<Complaint> Complaints { get; private set; } = new HashSet<Complaint>();
    public ICollection<Enumeration> Enumerations { get; private set; } = new HashSet<Enumeration>();
    public ICollection<BillDistribution> BillDistributions { get; private set; } = new HashSet<BillDistribution>();
    public ICollection<Evaluation> Evaluations { get; private set; } = new HashSet<Evaluation>();
    public ICollection<Disconnection> Disconnections { get; private set; } = new HashSet<Disconnection>();
    public ICollection<Reconnection> Reconnections { get; private set; } = new HashSet<Reconnection>();
    public Office? Office { get; private set; }
    #endregion

    #region Constructors
    private Staff()
    {
    }

    public Staff(DefaultIdType officeId, DefaultIdType applicationUserId, string city, string state, string lGA, string uniqueStaffId, bool isSuperAdmin = false)
    {
        OfficeId = officeId;
        ApplicationUserId = applicationUserId;
        City = city;
        State = state;
        LGA = lGA;
        StaffNumber = uniqueStaffId;
        IsSuperAdmin = isSuperAdmin;
    }
    #endregion
}
