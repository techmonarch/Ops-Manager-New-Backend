using OpsManagerAPI.Application.Features.Tariffs.Dtos;

namespace OpsManagerAPI.Application.Features.Tariffs.Queries;
public interface ITariffQueries : ITransientService
{
    Task<ApiResponse<List<TariffDetailsDto>>> GetAll(CancellationToken cancellationToken);
}
