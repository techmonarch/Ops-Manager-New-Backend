using OpsManagerAPI.Application.Features.Payments.Dtos;
using OpsManagerAPI.Domain.Aggregates.PaymentAggregate;
using System.Linq.Expressions;

namespace OpsManagerAPI.Application.Features.Payments.Queries;

public interface IPaymentRepository : ITransientService
{
    Task<(List<PaymentDto> Payments, int TotalCount)> GetAll(GetPaymentQuery request, CancellationToken cancellationToken);
}