using OpsManagerAPI.Application.Features.Disconnections.Dtos;

namespace OpsManagerAPI.Application.Features.Disconnections.Queries;

public class DisconnectionQueries : IDisconnectionQueries
{
    private readonly IDapperRepository _dapperRepository;
    private readonly IDisconnectionRepository _disconnectionRepository;

    public DisconnectionQueries(IDapperRepository dapperRepository, IDisconnectionRepository disconnectionRepository)
    {
        _dapperRepository = dapperRepository;
        _disconnectionRepository = disconnectionRepository;
    }

    public async Task<ApiResponse<PaginationResponse<DisconnectionCustomersDetailDto>>> GetAllDisconnectionCustomers(DisconnectionFilterRequest request, CancellationToken cancellationToken)
    {
        var (disconnections, totalCount) = await _disconnectionRepository.GetPaginatedAsync(request, cancellationToken);

        var disconnectionDataList = disconnections.ConvertAll(disconnection =>
        {
            return new DisconnectionCustomersDetailDto
            {
                AccountNumber = disconnection.Customer.AccountNumber,
                MeterNumber = disconnection.Customer.MeterNumber,
                Tariff = disconnection.Customer.Tariff.TariffCode,
                DistributionTransformer = disconnection.Customer.DistributionTransformer.Name,
                FirstName = disconnection.Customer.FirstName,
                MiddleName = disconnection.Customer.MiddleName,
                LastName = disconnection.Customer.LastName,
                Phone = disconnection.Customer.Phone,
                Email = disconnection.Customer.Email,
                Address = disconnection.Customer.Address,
                City = disconnection.Customer.City,
                State = disconnection.Customer.State,
                LGA = disconnection.Customer.LGA,
                Longitude = disconnection.Customer.Longitude,
                Latitude = disconnection.Customer.Latitude,
                CustomerType = disconnection.Customer.CustomerType.ToString(),
                AccountType = disconnection.Customer.AccountType.ToString(),
                DisconnectionStatus = disconnection.Status.ToString(),
                Reason = disconnection.Reason,
                DateLogged = disconnection.DateLogged,
                DateApproved = disconnection.DateApproved,
                DateDisconnected = disconnection.DateDisconnected,
                DisconnectionId = disconnection.Id,
                AmountOwed = disconnection.AmountOwed,
                AmountToPay = disconnection.AmountToPay
            };
        });

        // Create the pagination response
        var paginationResponse = new PaginationResponse<DisconnectionCustomersDetailDto>(
            disconnectionDataList.ToList(),
            totalCount,
            request.PageNumber,
            request.PageSize);
        return new ApiResponse<PaginationResponse<DisconnectionCustomersDetailDto>>(true, "Disconnection data retrieved successfully", paginationResponse);
    }
}
