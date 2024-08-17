using FirebaseAdmin.Messaging;
using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.Notifications.Commands;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;

public class NotificationsController : VersionNeutralApiController
{
    [HttpPost]
    [MustHavePermission(OPSAction.Create, OPSResource.Tenants)]
    [OpenApiOperation("Create A firebase Notification.", "")]
    public Task<ApiResponse<BatchResponse>> CreateAsync([FromForm] CreateNotificationCommand request)
    {
        return Mediator.Send(request);
    }
}