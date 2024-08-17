using OpsManagerAPI.Application.Features.Multitenancy.Contracts;

namespace OpsManagerAPI.Application.Features.Multitenancy.Commands;

public class ActivateTenantRequest : IRequest<ApiResponse<string>>
{
    public string TenantId { get; set; } = default!;

    public ActivateTenantRequest(string tenantId) => TenantId = tenantId;
}

public class ActivateTenantRequestValidator : CustomValidator<ActivateTenantRequest>
{
    public ActivateTenantRequestValidator() =>
        RuleFor(t => t.TenantId)
            .NotEmpty();
}

public class ActivateTenantRequestHandler : IRequestHandler<ActivateTenantRequest, ApiResponse<string>>
{
    private readonly IDiscoService _tenantService;

    public ActivateTenantRequestHandler(IDiscoService tenantService) => _tenantService = tenantService;

    public async Task<ApiResponse<string>> Handle(ActivateTenantRequest request, CancellationToken cancellationToken) =>
        await _tenantService.ActivateAsync(request.TenantId);
}