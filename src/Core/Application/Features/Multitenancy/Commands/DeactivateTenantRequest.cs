using OpsManagerAPI.Application.Features.Multitenancy.Contracts;

namespace OpsManagerAPI.Application.Features.Multitenancy.Commands;

public class DeactivateTenantRequest : IRequest<ApiResponse<string>>
{
    public string TenantId { get; set; } = default!;

    public DeactivateTenantRequest(string tenantId) => TenantId = tenantId;
}

public class DeactivateTenantRequestValidator : CustomValidator<DeactivateTenantRequest>
{
    public DeactivateTenantRequestValidator() =>
        RuleFor(t => t.TenantId)
            .NotEmpty();
}

public class DeactivateTenantRequestHandler : IRequestHandler<DeactivateTenantRequest, ApiResponse<string>>
{
    private readonly IDiscoService _tenantService;

    public DeactivateTenantRequestHandler(IDiscoService tenantService) => _tenantService = tenantService;

    public async Task<ApiResponse<string>> Handle(DeactivateTenantRequest request, CancellationToken cancellationToken) =>
        await _tenantService.DeactivateAsync(request.TenantId);
}