using OpsManagerAPI.Application.Features.MeterReadings.Dtos;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using System.Linq.Expressions;

namespace OpsManagerAPI.Application.Features.MeterReadings.Queries.QueryBuilders;
public static class MeterReadingQueryBuilder
{
    public static Expression<Func<MeterReading, bool>> BuildFilter(MeterReadingFilterRequest request)
    {
        return customer =>
            (request.CreatedBy == null || customer.CreatedBy == DefaultIdType.Parse(request.CreatedBy)) &&
            (request.ApprovalStatus == null || customer.IsApproved == request.ApprovalStatus) &&
            (request.StartDate == null || customer.LastModifiedOn >= request.StartDate) &&
            (request.MeterReadingType == null || customer.MeterReadingType == request.MeterReadingType) &&
            (request.EndDate == null || customer.LastModifiedOn <= request.EndDate);
    }
}