using OpsManagerAPI.Application.Features.Billings.Dtos;

namespace OpsManagerAPI.Application.Features.Billings.Queries;
public interface IBillingRepository : ITransientService
{
    Task<(List<BillingDetailsDto> Bills, int TotalCount)> GetAll(GetBillingQueries request, CancellationToken cancellationToken);
}