using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.Reconnections.Commands;
using OpsManagerAPI.Application.Features.Reconnections.Dtos;
using OpsManagerAPI.Application.Features.Reconnections.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;

public class ReconnectionsController : VersionNeutralApiController
{
    private readonly IReconnectionQueries _reconnectionService;
    public ReconnectionsController(IReconnectionQueries ReconnectionService)
       => _reconnectionService = ReconnectionService;

    /// <summary>
    /// Post a new Reconnection.
    /// </summary>
    /// <param name="request">The Reconnection details.</param>
    /// <returns>The ID of the created Reconnection.</returns>
    [HttpPost("create-ticket")]
    [MustHavePermission(OPSAction.Create, OPSResource.Reconnections)]
    [OpenApiOperation("Post a new Reconnection.", "Creates a new Reconnection with the provided details.")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<DefaultIdType>> CreateAsync([FromForm] CreateReconnectionTicketCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Approve a Reconnection by ID.
    /// </summary>
    /// <param name="id">The ID of the Reconnection.</param>
    /// <returns>The ID of the approved Reconnection.</returns>
    [HttpPut("{id}/approve")]
    [MustHavePermission(OPSAction.Update, OPSResource.Reconnections)]
    [OpenApiOperation("Approve Reconnection by ID.", "Approve a Reconnection using their unique identifier.")]
    public Task<ApiResponse<DefaultIdType>> ApproveAsync(DefaultIdType id)
    {
        return Mediator.Send(new ApproveReconnectionTicketCommand { ReconnectionTicketId = id });
    }

    /// <summary>
    /// DisApprove a Reconnection by ID.
    /// </summary>
    /// <param name="id">The ID of the Reconnection.</param>
    /// <returns>The ID of the dis-approved Reconnection.</returns>
    [HttpPut("{id}/dis-approve")]
    [MustHavePermission(OPSAction.Update, OPSResource.Reconnections)]
    [OpenApiOperation("DisApprove Reconnection by ID.", "DisApprove a Reconnection using their unique identifier.")]
    public Task<ApiResponse<DefaultIdType>> DisApproveAsync(DefaultIdType id)
    {
        return Mediator.Send(new DisApproveReconnectionTicketCommand { ReconnectionTicketId = id });
    }

    /// <summary>
    /// Close a Reconnection by ID.
    /// </summary>
    /// <returns>The ID of the close Reconnection.</returns>
    [HttpPut("close")]
    [MustHavePermission(OPSAction.Update, OPSResource.Reconnections)]
    [OpenApiOperation("Close Reconnection by ID.", "Close a Reconnection using their unique identifier.")]
    public Task<ApiResponse<DefaultIdType>> CloseAsync([FromForm] CloseReconnectionTicketCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Get reconnection customers.
    /// </summary>
    /// <param name="request">The pagination filter request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of reconnection customers.</returns>
    [HttpGet]
    [MustHavePermission(OPSAction.View, OPSResource.Reconnections)]
    [OpenApiOperation("Get all reconnection customers.", "Retrieve a paginated list of reconnection customers based on filter criteria.")]
    public Task<ApiResponse<PaginationResponse<ReconnectionCustomerDetailsDto>>> GetPendingReadinngsAsync([FromQuery] ReconnectionFilterRequest request, CancellationToken cancellationToken)
    {
        return _reconnectionService.GetAllReconnectionCustomers(request, cancellationToken);
    }
}