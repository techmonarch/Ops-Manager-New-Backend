using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.Multitenancy.Commands;
using OpsManagerAPI.Application.Features.Multitenancy.Dtos;
using OpsManagerAPI.Application.Features.Multitenancy.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;
public class DistributionCompaniesController : VersionNeutralApiController
{
    [HttpGet]
    [MustHavePermission(OPSAction.View, OPSResource.Tenants)]
    [OpenApiOperation("Get a list of all distribution companies.", "")]
    public Task<ApiResponse<List<DiscoDto>>> GetListAsync()
    {
        return Mediator.Send(new GetAllDiscosRequest());
    }

    [HttpGet("{id}")]
    [MustHavePermission(OPSAction.View, OPSResource.Tenants)]
    [OpenApiOperation("Get distribution company's details.", "")]
    public Task<ApiResponse<DiscoDto>> GetAsync(string id)
    {
        return Mediator.Send(new GetDiscoRequest(id));
    }

    [HttpPost]
    [MustHavePermission(OPSAction.Create, OPSResource.Tenants)]
    [OpenApiOperation("Create a new distribution company.", "")]
    public Task<ApiResponse<string>> CreateAsync(CreateDiscoRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("{id}/activate")]
    [MustHavePermission(OPSAction.Update, OPSResource.Tenants)]
    [OpenApiOperation("Activate a distribution company.", "")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<string>> ActivateAsync(string id)
    {
        return Mediator.Send(new ActivateTenantRequest(id));
    }

    [HttpPost("{id}/deactivate")]
    [MustHavePermission(OPSAction.Update, OPSResource.Tenants)]
    [OpenApiOperation("Deactivate a distribution company.", "")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<string>> DeactivateAsync(string id)
    {
        return Mediator.Send(new DeactivateTenantRequest(id));
    }

    [HttpPost("{id}/upgrade")]
    [MustHavePermission(OPSAction.UpgradeSubscription, OPSResource.Tenants)]
    [OpenApiOperation("Upgrade a distribution company's subscription.", "")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public async Task<ActionResult<ApiResponse<string>>> UpgradeSubscriptionAsync(string id, UpgradeSubscriptionRequest request)
    {
        return id != request.TenantId
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }
}