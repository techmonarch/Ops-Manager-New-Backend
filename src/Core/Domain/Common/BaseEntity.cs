using System.ComponentModel.DataAnnotations.Schema;
using OpsManagerAPI.Domain.Util;

namespace OpsManagerAPI.Domain.Common;
public abstract class BaseEntity : BaseEntity<DefaultIdType>
{
    protected BaseEntity() => Id = SequentialGuid.SqlServer.NewGuid();
}

public abstract class BaseEntity<TId> : IEntity<TId>
{
    public TId Id { get; protected set; } = default!;
}