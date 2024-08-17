using OpsManagerAPI.Application.Features.Evaluations.Dtos;
using OpsManagerAPI.Domain.Aggregates.BillingAggregate;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;

namespace OpsManagerAPI.Application.Features.Evaluations.Queries;

public interface IEvaluationRepository : ITransientService
{
    Task<List<Evaluation>> SearchAsync(EvaluationFilterRequest request, CancellationToken cancellationToken);
}
