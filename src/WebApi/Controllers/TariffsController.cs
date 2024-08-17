using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.Tariffs.Dtos;
using OpsManagerAPI.Application.Features.Tariffs.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;

public class TariffsController : VersionNeutralApiController
{
    private readonly ITariffQueries _tariffQueries;
    public TariffsController(ITariffQueries tariffQueries)
       => _tariffQueries = tariffQueries;

    /// <summary>
    /// Get all tariffs.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of bill distributions details.</returns>
    [HttpGet]
    [MustHavePermission(OPSAction.View, OPSResource.Tariffs)]
    [OpenApiOperation("Get all tariffs.", "Retrieve a list of all available tariffs.")]
    public async Task<ApiResponse<List<TariffDetailsDto>>> SearchAsync(CancellationToken cancellationToken)
    {
        return await _tariffQueries.GetAll(cancellationToken);
    }
}
