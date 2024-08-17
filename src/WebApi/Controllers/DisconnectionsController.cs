using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.Disconnections.Commands;
using OpsManagerAPI.Application.Features.Disconnections.Dtos;
using OpsManagerAPI.Application.Features.Disconnections.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;

public class DisconnectionsController : VersionNeutralApiController
{
    private readonly IDisconnectionQueries _disconnectionService;
    public DisconnectionsController(IDisconnectionQueries disconnectionService)
       => _disconnectionService = disconnectionService;

    /// <summary>
    /// Post a new disconnection.
    /// </summary>
    /// <param name="request">The disconnection details.</param>
    /// <returns>The ID of the created disconnection.</returns>
    [HttpPost("create-ticket")]
    [MustHavePermission(OPSAction.Create, OPSResource.Disconnections)]
    [OpenApiOperation("Post a new disconnection.", "Creates a new disconnection with the provided details.")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<DefaultIdType>> CreateAsync([FromForm] CreateDisconnectionTicketCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Approve a disconnection by ID.
    /// </summary>
    /// <param name="id">The ID of the disconnection.</param>
    /// <returns>The ID of the approved disconnection.</returns>
    [HttpPut("{id}/approve")]
    [MustHavePermission(OPSAction.Update, OPSResource.Disconnections)]
    [OpenApiOperation("Approve disconnection by ID.", "Approve a disconnection using their unique identifier.")]
    public Task<ApiResponse<DefaultIdType>> ApproveAsync(DefaultIdType id)
    {
        return Mediator.Send(new ApproveDisconnectionTicketCommand { DisconnectionTicketId = id });
    }

    /// <summary>
    /// DisApprove a disconnection by ID.
    /// </summary>
    /// <param name="id">The ID of the disconnection.</param>
    /// <returns>The ID of the dis-approved disconnection.</returns>
    [HttpPut("{id}/dis-approve")]
    [MustHavePermission(OPSAction.Update, OPSResource.Disconnections)]
    [OpenApiOperation("DisApprove disconnection by ID.", "DisApprove a disconnection using their unique identifier.")]
    public Task<ApiResponse<DefaultIdType>> DisApproveAsync(DefaultIdType id)
    {
        return Mediator.Send(new DisApproveDisconnectionTicketCommand { DisconnectionTicketId = id });
    }

    /// <summary>
    /// Close a disconnection by ID.
    /// </summary>
    /// <returns>The ID of the close disconnection.</returns>
    [HttpPut("close")]
    [MustHavePermission(OPSAction.Update, OPSResource.Disconnections)]
    [OpenApiOperation("Close disconnection by ID.", "Close a disconnection using their unique identifier.")]
    public Task<ApiResponse<DefaultIdType>> CloseAsync([FromForm] CloseDisconnectionTicketCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Get disconnection customers.
    /// </summary>
    /// <param name="request">The pagination filter request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of disconnection customers.</returns>
    [HttpGet]
    [MustHavePermission(OPSAction.View, OPSResource.Disconnections)]
    [OpenApiOperation("Get all disconnection customers.", "Retrieve a paginated list of disconnection customers based on filter criteria.")]
    public Task<ApiResponse<PaginationResponse<DisconnectionCustomersDetailDto>>> GetPendingReadingsAsync([FromQuery] DisconnectionFilterRequest request, CancellationToken cancellationToken)
    {
        return _disconnectionService.GetAllDisconnectionCustomers(request, cancellationToken);
    }
}
