using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.DisconnectionReasons.Commands;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;

public class DisconnectionReasonsController : VersionNeutralApiController
{
    /// <summary>
    /// Post a new disconnection reasoon.
    /// </summary>
    /// <param name="request">The disconnection details.</param>
    /// <returns>The ID of the created disconnection.</returns>
    [HttpPost]
    [MustHavePermission(OPSAction.Create, OPSResource.DisconnectionReasons)]
    [OpenApiOperation("Post a new disconnection Reason.", "Creates a new disconnection Reason with the provided details.")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<DefaultIdType>> CreateAsync(CreateDisconnectionReasonCommand request)
    {
        return Mediator.Send(request);
    }
}