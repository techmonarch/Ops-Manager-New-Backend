using OpsManagerAPI.Application.Features.Customers.Dtos;
using OpsManagerAPI.Domain.Aggregates.CustomerAggregate;
using System.Linq.Expressions;

namespace OpsManagerAPI.Application.Features.Customers.Queries.QueryBuilders;
public static class CustomerQueryBuilder
{
    public static Expression<Func<Customer, bool>> BuildFilter(CustomerFilterRequest request)
    {
        return customer =>
            (string.IsNullOrEmpty(request.Name) ||
                (customer.FirstName != null && customer.FirstName.Contains(request.Name, StringComparison.OrdinalIgnoreCase)) ||
                (customer.MiddleName != null && customer.MiddleName.Contains(request.Name, StringComparison.OrdinalIgnoreCase)) ||
                (customer.LastName != null && customer.LastName.Contains(request.Name, StringComparison.OrdinalIgnoreCase))) &&
            (string.IsNullOrEmpty(request.Region) ||
                (customer.State != null && customer.State.Contains(request.Region, StringComparison.OrdinalIgnoreCase))) &&
            (string.IsNullOrEmpty(request.Hub) ||
                (customer.City != null && customer.City.Contains(request.Hub, StringComparison.OrdinalIgnoreCase))) &&
            (string.IsNullOrEmpty(request.ServiceCenter) ||
                (customer.LGA != null && customer.LGA.Contains(request.ServiceCenter, StringComparison.OrdinalIgnoreCase))) &&
            (string.IsNullOrEmpty(request.PhoneNumber) ||
                (customer.Phone != null && customer.Phone.Contains(request.PhoneNumber))) &&
            (string.IsNullOrEmpty(request.AccountNumber) ||
                (customer.AccountNumber != null && customer.AccountNumber.Contains(request.AccountNumber))) &&
            (string.IsNullOrEmpty(request.MeterNumber) ||
                (customer.MeterNumber != null && customer.MeterNumber.Contains(request.MeterNumber))) &&
            (string.IsNullOrEmpty(request.Email) ||
                (customer.Email != null && customer.Email.Contains(request.Email))) &&
            (request.Status == null || customer.Status == request.Status) &&
            (request.Id == null || customer.Id == request.Id) &&
            (request.AccountType == null || customer.AccountType == request.AccountType) &&
            (request.CustomerType == null || customer.CustomerType == request.CustomerType) &&
            (request.DistributionTransformerId == null ||
                (customer.DistributionTransformer != null && customer.DistributionTransformer.Id == request.Id)) &&
            (request.ServiceBand == null ||
                (customer.Tariff != null && customer.Tariff.ServiceBandName != null && customer.Tariff.ServiceBandName.Contains(request.ServiceBand, StringComparison.OrdinalIgnoreCase)));
    }
}