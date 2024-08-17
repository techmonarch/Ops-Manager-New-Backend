using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.BillDistributions.Commands;
using OpsManagerAPI.Application.Features.BillDistributions.Dtos;
using OpsManagerAPI.Application.Features.BillDistributions.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;
public class BillDistributionsController : VersionNeutralApiController
{
    private readonly IBillDistributionQueries _billDistributionQueries;
    public BillDistributionsController(IBillDistributionQueries billDistributionQueries)
       => _billDistributionQueries = billDistributionQueries;

    /// <summary>
    /// Post a new bill distribution.
    /// </summary>
    /// <param name="request">The bill distribution details.</param>
    /// <returns>The ID of the created bill distribution.</returns>
    [HttpPost]
    [MustHavePermission(OPSAction.Create, OPSResource.BillDistributions)]
    [OpenApiOperation("Post a new bill distribution.", "Creates a new bill distribution with the provided details.")]
    public Task<ApiResponse<DefaultIdType>> CreateAsync(DistributeBillCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Search for bill distributions.
    /// </summary>
    /// <param name="request">The bill distributions filter request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of bill distributions details.</returns>
    [HttpGet("search")]
    [MustHavePermission(OPSAction.View, OPSResource.BillDistributions)]
    [OpenApiOperation("Search for bill distributions.", "Retrieve a paginated list of bill distributions based on filter criteria.")]
    public async Task<ApiResponse<PaginationResponse<BillDistributionDetailDto>>> SearchAsync([FromQuery] BillDistributionFilterRequest request, CancellationToken cancellationToken)
    {
        return await _billDistributionQueries.SearchAsync(request, cancellationToken);
    }

    /// <summary>
    /// View Dashboard for bill distributions.
    /// </summary>
    /// <returns>Retrieve a statistical dashboard details for Bill Distributions.</returns>
    [HttpGet("dashboard")]
    [MustHavePermission(OPSAction.View, OPSResource.BillDistributions)]
    [OpenApiOperation("View Dashboard for bill distributions.", "Retrieve a statistical dashboard details for Bill Distributions.")]
    public async Task<ApiResponse<BillDistributionDashboardDto>> Dashboard() => await _billDistributionQueries.Dashboard();
}