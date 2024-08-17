using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.Enumerations.Commands;
using OpsManagerAPI.Application.Features.Enumerations.Dtos;
using OpsManagerAPI.Application.Features.Enumerations.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;
public class EnumerationsController : VersionNeutralApiController
{
    private readonly IEnumerationsQueries _enumerationQueries;
    public EnumerationsController(IEnumerationsQueries enumerationQueries) => _enumerationQueries = enumerationQueries;

    /// <summary>
    /// Post a new enumeration.
    /// </summary>
    /// <param name="request">The enumeration details.</param>
    /// <returns>The ID of the created enumeration.</returns>
    [HttpPost]
    [MustHavePermission(OPSAction.Create, OPSResource.Enumerations)]
    [OpenApiOperation("Post a new enumeration.", "Creates a new enumeration with the provided details.")]
    public Task<ApiResponse<DefaultIdType>> CreateAsync([FromForm] CreateEnumerationCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Search for enumerations.
    /// </summary>
    /// <param name="request">The enumerations filter request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of enumerations details.</returns>
    [HttpGet("search")]
    [MustHavePermission(OPSAction.View, OPSResource.Enumerations)]
    [OpenApiOperation("Search for enumerations.", "Retrieve a paginated list of enumerations based on filter criteria.")]
    public async Task<ApiResponse<PaginationResponse<EnumerationDetailDto>>> SearchAsync([FromQuery] EnumerationFilterRequest request, CancellationToken cancellationToken)
    {
        return await _enumerationQueries.SearchAsync(request, cancellationToken);
    }

    /// <summary>
    /// View Dashboard for enumerations.
    /// </summary>
    /// <returns>Retrieve a statistical dashboard details for enumerations.</returns>
    [HttpGet("dashboard")]
    [MustHavePermission(OPSAction.View, OPSResource.Enumerations)]
    [OpenApiOperation("View Dashboard for enumerations.", "Retrieve a statistical dashboard details for enumerations.")]
    public async Task<ApiResponse<EnumerationDashboardDto>> Dashboard() => await _enumerationQueries.Dashboard();
}