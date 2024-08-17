namespace OpsManagerAPI.Application.Features.Customers.Dtos;

public record CustomerDataDto(
    string Id,
    string AccountNumber,
    string? MeterNumber,
    string Phone,
    string City,
    string State,
    string ServiceBand,
    string CustomerType,
    string AccountType,
    string Status,
    string FirstName,
    string LastName,
    string MiddleName,
    string Address,
    string Email,
    string DistributionTransformerId
) : IDto;
