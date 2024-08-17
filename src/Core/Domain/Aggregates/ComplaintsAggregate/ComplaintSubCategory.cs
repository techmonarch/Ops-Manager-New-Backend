using OpsManagerAPI.Domain.Common;

namespace OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;

public class ComplaintSubCategory : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public DefaultIdType CategoryId { get; private set; }

    #region Constructor
    public ComplaintSubCategory(string name, DefaultIdType categoryId)
    {
        Name = name;
        CategoryId = categoryId;
    }
    #endregion
}
