using OpsManagerAPI.Domain.Common;

namespace OpsManagerAPI.Domain.Aggregates.OfficeAggregate;

public class OfficeLevel : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public bool IsActive { get; private set; }
    public int LevelId { get; private set; }

    #region Relationships
    public ICollection<Office> Stores { get; set; } = new HashSet<Office>();
    #endregion

    #region Constructors
    private OfficeLevel()
    {
    }

    public OfficeLevel(string name, bool isActive, int levelId)
    {
        Name = name;
        IsActive = isActive;
        LevelId = levelId;
    }
    #endregion
}
