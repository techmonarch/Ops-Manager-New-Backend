using Ardalis.Specification.EntityFrameworkCore;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Application.Features.Complaints.Dtos;
using OpsManagerAPI.Application.Features.Payments.Dtos;
using OpsManagerAPI.Application.Features.Payments.Queries;
using OpsManagerAPI.Application.Features.Payments.Queries.QueryBuilders;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using OpsManagerAPI.Domain.Aggregates.PaymentAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using System.Threading;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;
public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _dbContext;
    public PaymentRepository(ApplicationDbContext applicationDbContext) => _dbContext = applicationDbContext;

    public async Task<(List<PaymentDto> Payments, int TotalCount)> GetAll(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpec<Payment>(request);

        var filter = PaymentQueryBuilder.BuildFilter(request);
        int totalCount = await _dbContext.Payments
                          .AsNoTracking()
                          .Where(filter)
                          .CountAsync(cancellationToken: cancellationToken);

        var paymentPagination = _dbContext.Payments
                                  .AsNoTracking()
                                  .Where(filter);

        if (request.StartDate != null && request.EndDate != null)
        {
            paymentPagination = paymentPagination
                .Where(s => s.PaymentDate >= request.StartDate && s.PaymentDate <= request.EndDate);
        }
        else
        {
            DateTime sixMonthsPayment = DateTime.UtcNow.AddMonths(-6);
            paymentPagination = paymentPagination
                .Where(s => s.PaymentDate >= sixMonthsPayment);
        }

        var payments = (await paymentPagination
            .WithSpecification(spec)
            .OrderByDescending(s => s.PaymentDate)
            .ToListAsync(cancellationToken: cancellationToken))
            .Adapt<List<PaymentDto>>();

        return (payments, totalCount);
    }
}
