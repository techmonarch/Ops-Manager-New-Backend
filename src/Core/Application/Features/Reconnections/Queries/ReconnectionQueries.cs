using OpsManagerAPI.Application.Features.Downloads.Queries;
using OpsManagerAPI.Application.Features.Reconnections.Dtos;

namespace OpsManagerAPI.Application.Features.Reconnections.Queries;
public class ReconnectionQueries : IReconnectionQueries
{
    private readonly IReconnectionRepository _reconnectionRepository;

    public ReconnectionQueries(IReconnectionRepository reconnectionRepository)
        => _reconnectionRepository = reconnectionRepository;

    public async Task<ApiResponse<PaginationResponse<ReconnectionCustomerDetailsDto>>> GetAllReconnectionCustomers(ReconnectionFilterRequest request, CancellationToken cancellationToken)
    {
        var (reconnections, totalCount) = await _reconnectionRepository.GetPaginatedAsync(request, cancellationToken);

        var reconnectionDataList = reconnections.ConvertAll(reconnection =>
        {
            return new ReconnectionCustomerDetailsDto
            {
                AccountNumber = reconnection.Customer.AccountNumber,
                MeterNumber = reconnection.Customer.MeterNumber,
                Tariff = reconnection.Customer.Tariff.TariffCode,
                DistributionTransformer = reconnection.Customer.DistributionTransformer.Name,
                FirstName = reconnection.Customer.FirstName,
                MiddleName = reconnection.Customer.MiddleName,
                LastName = reconnection.Customer.LastName,
                Phone = reconnection.Customer.Phone,
                Email = reconnection.Customer.Email,
                Address = reconnection.Customer.Address,
                City = reconnection.Customer.City,
                State = reconnection.Customer.State,
                LGA = reconnection.Customer.LGA,
                Longitude = reconnection.Customer.Longitude,
                Latitude = reconnection.Customer.Latitude,
                CustomerType = reconnection.Customer.CustomerType.ToString(),
                AccountStatus = reconnection.Customer.Status.ToString(),
                AccountType = reconnection.Customer.AccountType.ToString(),
                ReconnectionStatus = reconnection.Status.ToString(),
                Reason = reconnection.Reason,
                DateReconnected = reconnection.DateReconnected,
                DateLogged = reconnection.DateLogged,
                DateApproved = reconnection.DateApproved,
                ReconnectionId = reconnection.Id,
            };
        });

        // Create the pagination response
        var paginationResponse = new PaginationResponse<ReconnectionCustomerDetailsDto>(
            reconnectionDataList.ToList(),
            totalCount,
            request.PageNumber,
            request.PageSize);
        return new ApiResponse<PaginationResponse<ReconnectionCustomerDetailsDto>>(true, "Reconnection data retrieved successfully", paginationResponse);
    }
}
