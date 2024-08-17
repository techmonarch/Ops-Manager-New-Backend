using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.MeterReadings.Commands;
using OpsManagerAPI.Application.Features.MeterReadings.Dtos;
using OpsManagerAPI.Application.Features.MeterReadings.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;
public class MeterReadingsController : VersionNeutralApiController
{
    private readonly IMeterReadingQueries _meterReadingService;
    public MeterReadingsController(IMeterReadingQueries meterReadingService)
       => _meterReadingService = meterReadingService;

    /// <summary>
    /// Post a new meter reading.
    /// </summary>
    /// <param name="request">The meter reading details.</param>
    /// <returns>The ID of the created meter reading.</returns>
    [HttpPost]
    [MustHavePermission(OPSAction.Create, OPSResource.MeterReadings)]
    [OpenApiOperation("Post a new meter reading.", "Creates a new meter reading with the provided details.")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<DefaultIdType>> CreateAsync([FromForm] CreateMeterReadingCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Approve a meter reading by ID.
    /// </summary>
    /// <param name="id">The ID of the meter reading.</param>
    /// <returns>The ID of the approved meter reading.</returns>
    [HttpPut("{id}/approve")]
    [MustHavePermission(OPSAction.Update, OPSResource.MeterReadings)]
    [OpenApiOperation("Approve meter reading by ID.", "Approve a meter reading using their unique identifier.")]
    public Task<ApiResponse<DefaultIdType>> ApproveAsync(DefaultIdType id)
    {
        return Mediator.Send(new ApproveMeterReadingCommand { MeterReadingId = id });
    }

    /// <summary>
    /// DisApprove a meter reading by ID.
    /// </summary>
    /// <param name="id">The ID of the meter reading.</param>
    /// <returns>The ID of the dis-approved meter reading.</returns>
    [HttpPut("{id}/dis-approve")]
    [MustHavePermission(OPSAction.Update, OPSResource.MeterReadings)]
    [OpenApiOperation("DisApprove meter reading by ID.", "DisApprove a meter reading using their unique identifier.")]
    public Task<ApiResponse<DefaultIdType>> DisApproveAsync(DefaultIdType id)
    {
        return Mediator.Send(new DisApproveMeterReadingCommand { MeterReadingId = id });
    }

    /// <summary>
    /// Search for meter reading.
    /// </summary>
    /// <param name="request">The meter reading filter request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of meter reading details.</returns>
    [HttpGet("search")]
    [MustHavePermission(OPSAction.View, OPSResource.MeterReadings)]
    [OpenApiOperation("Search for meter reading.", "Retrieve a paginated list of meter reading based on filter criteria.")]
    public Task<ApiResponse<PaginationResponse<MeterReadingDetailDto>>> SearchAsync([FromQuery] MeterReadingFilterRequest request, CancellationToken cancellationToken)
    {
        return _meterReadingService.SearchAsync(request, cancellationToken);
    }

    /// <summary>
    /// Get pending meter reading.
    /// </summary>
    /// <param name="request">The pagination filter request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of pending meter reading.</returns>
    [HttpGet("pending-readings")]
    [MustHavePermission(OPSAction.View, OPSResource.MeterReadings)]
    [OpenApiOperation("Get pending meter reading.", "Retrieve a paginated list of pending meter reading based on filter criteria.")]
    public Task<ApiResponse<PaginationResponse<MeterReadingDetailDto>>> GetPendingReadinngsAsync([FromQuery] PaginationFilter request, CancellationToken cancellationToken)
    {
        return _meterReadingService.GetPendingReadinngsAsync(request, cancellationToken);
    }

    /// <summary>
    /// View Dashboard for meter readings.
    /// </summary>
    /// <returns>Retrieve a statistical dashboard details for meter readings.</returns>
    [HttpGet("dashboard")]
    [MustHavePermission(OPSAction.View, OPSResource.MeterReadings)]
    [OpenApiOperation("View Dashboard for meter readings.", "Retrieve a statistical dashboard details for meter readings.")]
    public async Task<ApiResponse<MeterReadingDashboardDto>> Dashboard() => await _meterReadingService.Dashboard();
}