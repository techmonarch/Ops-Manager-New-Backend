namespace OpsManagerAPI.Domain.Common.Contracts;

public interface IEntity<TId> : IEntity
{
    TId Id { get; }
}

public interface IEntity
{
}