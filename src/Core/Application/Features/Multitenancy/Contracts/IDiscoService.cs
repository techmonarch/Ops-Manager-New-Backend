using OpsManagerAPI.Application.Features.Multitenancy.Commands;
using OpsManagerAPI.Application.Features.Multitenancy.Dtos;

namespace OpsManagerAPI.Application.Features.Multitenancy.Contracts;

public interface IDiscoService
{
    Task<ApiResponse<List<DiscoDto>>> GetAllAsync();
    Task<bool> ExistsWithIdAsync(string id);
    Task<bool> ExistsWithNameAsync(string name);
    Task<ApiResponse<DiscoDto>> GetByIdAsync(string id);
    Task<ApiResponse<string>> CreateAsync(CreateDiscoRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<string>> ActivateAsync(string id);
    Task<ApiResponse<string>> DeactivateAsync(string id);
    Task<ApiResponse<string>> UpdateSubscription(string id, DateTime extendedExpiryDate);
}