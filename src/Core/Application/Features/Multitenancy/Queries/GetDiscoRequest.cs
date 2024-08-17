using OpsManagerAPI.Application.Features.Multitenancy.Contracts;
using OpsManagerAPI.Application.Features.Multitenancy.Dtos;

namespace OpsManagerAPI.Application.Features.Multitenancy.Queries;

public class GetDiscoRequest : IRequest<ApiResponse<DiscoDto>>
{
    public string TenantId { get; set; } = default!;

    public GetDiscoRequest(string tenantId) => TenantId = tenantId;
}

public class GetTenantRequestValidator : CustomValidator<GetDiscoRequest>
{
    public GetTenantRequestValidator() =>
        RuleFor(t => t.TenantId)
            .NotEmpty();
}

public class GetTenantRequestHandler : IRequestHandler<GetDiscoRequest, ApiResponse<DiscoDto>>
{
    private readonly IDiscoService _tenantService;

    public GetTenantRequestHandler(IDiscoService tenantService) => _tenantService = tenantService;

    public Task<ApiResponse<DiscoDto>> Handle(GetDiscoRequest request, CancellationToken cancellationToken) =>
        _tenantService.GetByIdAsync(request.TenantId);
}