using OpsManagerAPI.Application.Features.Enumerations.Dtos;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using System.Linq.Expressions;

namespace OpsManagerAPI.Application.Features.Enumerations.Queries.QueryBuilders;
public static class EnumerationQueryBuilder
{
    public static Expression<Func<Enumeration, bool>> BuildFilter(EnumerationFilterRequest request)
    {
        return enumeration =>
            (request.CreatedBy == null || enumeration.CreatedBy == DefaultIdType.Parse(request.CreatedBy)) &&
            (request.StartDate == null || enumeration.LastModifiedOn >= request.StartDate) &&
            (request.Id == null || enumeration.Id == request.Id) &&
            (request.AccountNumber == null ||
             (enumeration.AccountNumber != null && enumeration.AccountNumber.Contains(request.AccountNumber))) &&
            (request.EndDate == null || enumeration.LastModifiedOn <= request.EndDate);
    }
}