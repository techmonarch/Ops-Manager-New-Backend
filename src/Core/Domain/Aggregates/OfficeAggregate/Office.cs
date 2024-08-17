using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;
using OpsManagerAPI.Domain.Common;

namespace OpsManagerAPI.Domain.Aggregates.OfficeAggregate;
public class Office : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public DefaultIdType OfficeLevelId { get; private set; }
    public bool IsActive { get; private set; }

    #region Relationships
    public OfficeLevel? OfficeLevel { get; set; }

    public ICollection<Staff> Staffs { get; set; } = new HashSet<Staff>();
    public ICollection<DistributionTransformer> DistributionTransformers { get; set; } = new HashSet<DistributionTransformer>();
    #endregion

    #region Constructors
    private Office()
    {
    }

    public Office(string name, DefaultIdType officeLevelId)
    {
        Name = name;
        OfficeLevelId = officeLevelId;
        IsActive = true;
    }
    #endregion

    #region Behaviours
    public void ToggleActiveStatus(bool isActive)
    {
        // prevent error while changing to same status
        if (IsActive == isActive) return;

        IsActive = isActive;
    }
    #endregion
}
