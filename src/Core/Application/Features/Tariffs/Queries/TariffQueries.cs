using OpsManagerAPI.Application.Features.Tariffs.Dtos;

namespace OpsManagerAPI.Application.Features.Tariffs.Queries;
public class TariffQueries : ITariffQueries
{
    #region Constructor
    private readonly ITariffRepository _repository;
    public TariffQueries(ITariffRepository repository) => _repository = repository;

    #endregion

    public async Task<ApiResponse<List<TariffDetailsDto>>> GetAll(CancellationToken cancellationToken)
    {
        var tariffs = await _repository.GetAll(cancellationToken);
        return new ApiResponse<List<TariffDetailsDto>>(true, "Tariff retrieved successfully.", tariffs);
    }
}
