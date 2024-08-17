using OpsManagerAPI.Infrastructure.Multitenancy;

namespace OpsManagerAPI.Infrastructure.Persistence.Initialization;
internal interface IDatabaseInitializer
{
    Task InitializeDatabasesAsync(CancellationToken cancellationToken);
    Task InitializeApplicationDbForDiscoAsync(DiscoInfo tenant, CancellationToken cancellationToken);
}