using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.DistributionTransformers.Dtos;
using OpsManagerAPI.Application.Features.DistributionTransformers.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;

public class DistributionTransformersController : VersionNeutralApiController
{
    /// <summary>
    /// Get for distribution transformers.
    /// </summary>
    /// <param name="request">The distribution transformers filter request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of distribution transformers details.</returns>
    [HttpGet]
    [MustHavePermission(OPSAction.View, OPSResource.DistributionTransformers)]
    [OpenApiOperation("Search for distribution transformers.", "Retrieve a paginated list of distribution transformers based on filter criteria.")]
    public async Task<ApiResponse<PaginationResponse<DistributionTransformerDetailDto>>> SearchAsync([FromQuery] GetDistributionTransformersQueries request, CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }
}
