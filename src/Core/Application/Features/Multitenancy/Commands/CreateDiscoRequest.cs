using OpsManagerAPI.Application.Features.Multitenancy.Contracts;

namespace OpsManagerAPI.Application.Features.Multitenancy.Commands;

public class CreateDiscoRequest : IRequest<ApiResponse<string>>
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? ConnectionString { get; set; }
    public string AdminEmail { get; set; } = default!;
    public string? Issuer { get; set; }
}

public class CreateTenantRequestHandler : IRequestHandler<CreateDiscoRequest, ApiResponse<string>>
{
    private readonly IDiscoService _tenantService;

    public CreateTenantRequestHandler(IDiscoService tenantService) => _tenantService = tenantService;

    public async Task<ApiResponse<string>> Handle(CreateDiscoRequest request, CancellationToken cancellationToken) =>
        await _tenantService.CreateAsync(request, cancellationToken);
}