using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.Evaluations.Commands;
using OpsManagerAPI.Application.Features.Evaluations.Dtos;
using OpsManagerAPI.Application.Features.Evaluations.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;
public class EvaluationsController : VersionNeutralApiController
{
    private readonly IEvaluationQueries _evaluationQueries;
    public EvaluationsController(IEvaluationQueries evaluationQueries)
       => _evaluationQueries = evaluationQueries;

    /// <summary>
    /// Post a new evaluation.
    /// </summary>
    /// <param name="request">The evaluation details.</param>
    /// <returns>The ID of the created evaluation.</returns>
    [HttpPost]
    [MustHavePermission(OPSAction.Create, OPSResource.Evaluations)]
    [OpenApiOperation("Post a new evaluation.", "Creates a new evaluation with the provided details.")]
    public Task<ApiResponse<DefaultIdType>> CreateAsync([FromForm] CreateEvaluationCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Search for evaluations.
    /// </summary>
    /// <param name="request">The evaluations filter request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of evaluations details.</returns>
    [HttpGet("search")]
    [MustHavePermission(OPSAction.View, OPSResource.Evaluations)]
    [OpenApiOperation("Search for evaluations.", "Retrieve a paginated list of evaluations based on filter criteria.")]
    public async Task<ApiResponse<PaginationResponse<EvaluationDetailDto>>> SearchAsync([FromQuery] EvaluationFilterRequest request, CancellationToken cancellationToken)
    {
        return await _evaluationQueries.SearchAsync(request, cancellationToken);
    }

    /// <summary>
    /// View Dashboard for evaluations.
    /// </summary>
    /// <returns>Retrieve a statistical dashboard details for evaluations.</returns>
    [HttpGet("dashboard")]
    [MustHavePermission(OPSAction.View, OPSResource.Evaluations)]
    [OpenApiOperation("View Dashboard for evaluations.", "Retrieve a statistical dashboard details for evaluations.")]
    public async Task<ApiResponse<EvaluationDashboardDto>> Dashboard() => await _evaluationQueries.Dashboard();
}