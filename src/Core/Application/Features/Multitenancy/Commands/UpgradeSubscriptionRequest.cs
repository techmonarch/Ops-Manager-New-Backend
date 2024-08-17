using OpsManagerAPI.Application.Features.Multitenancy.Contracts;

namespace OpsManagerAPI.Application.Features.Multitenancy.Commands;

public class UpgradeSubscriptionRequest : IRequest<ApiResponse<string>>
{
    public string TenantId { get; set; } = default!;
    public DateTime ExtendedExpiryDate { get; set; }
}

public class UpgradeSubscriptionRequestValidator : CustomValidator<UpgradeSubscriptionRequest>
{
    public UpgradeSubscriptionRequestValidator() =>
        RuleFor(t => t.TenantId)
            .NotEmpty();
}

public class UpgradeSubscriptionRequestHandler : IRequestHandler<UpgradeSubscriptionRequest, ApiResponse<string>>
{
    private readonly IDiscoService _tenantService;

    public UpgradeSubscriptionRequestHandler(IDiscoService tenantService) => _tenantService = tenantService;

    public async Task<ApiResponse<string>> Handle(UpgradeSubscriptionRequest request, CancellationToken cancellationToken) =>
         await _tenantService.UpdateSubscription(request.TenantId, request.ExtendedExpiryDate);
}