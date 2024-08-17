using OpsManagerAPI.Domain.Common;

namespace OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;

public class ComplaintCategory : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;

    #region Constructor
    public ComplaintCategory(string name) => Name = name;
    #endregion
}
