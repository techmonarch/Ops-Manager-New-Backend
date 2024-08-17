using OpsManagerAPI.Application.Features.Multitenancy.Contracts;
using OpsManagerAPI.Application.Features.Multitenancy.Dtos;

namespace OpsManagerAPI.Application.Features.Multitenancy.Queries;

public record GetAllDiscosRequest : IRequest<ApiResponse<List<DiscoDto>>>;

public class GetAllTenantsRequestHandler : IRequestHandler<GetAllDiscosRequest, ApiResponse<List<DiscoDto>>>
{
    private readonly IDiscoService _tenantService;

    public GetAllTenantsRequestHandler(IDiscoService tenantService) => _tenantService = tenantService;

    public Task<ApiResponse<List<DiscoDto>>> Handle(GetAllDiscosRequest request, CancellationToken cancellationToken) =>
        _tenantService.GetAllAsync();
}