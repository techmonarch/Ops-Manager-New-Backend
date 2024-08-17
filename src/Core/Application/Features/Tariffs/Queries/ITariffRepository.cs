using OpsManagerAPI.Application.Features.Tariffs.Dtos;

namespace OpsManagerAPI.Application.Features.Tariffs.Queries;
public interface ITariffRepository : ITransientService
{
    Task<List<TariffDetailsDto>> GetAll(CancellationToken cancellationToken);
}
