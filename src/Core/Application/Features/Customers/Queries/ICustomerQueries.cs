using OpsManagerAPI.Application.Features.Customers.Dtos;

namespace OpsManagerAPI.Application.Features.Customers.Queries;
public interface ICustomerQueries : ITransientService
{
    Task<ApiResponse<CustomerDetailDto>> GetAsync(string customerId, CancellationToken cancellationToken);
    Task<ApiResponse<CustomerDetailDto>> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken);
    Task<ApiResponse<PaginationResponse<CustomerDataDto>>> SearchAsync(CustomerFilterRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<PaginationResponse<MeterReadingsAndBillDistributionsCustomerDataDto>>> GetForMeterReadingsAndBillDistributions(CustomerFilterRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<PaginationResponse<EvaluationsAndEnumerationsCustomerDataDto>>> GetForEvaluationsAndEnumerations(CustomerFilterRequest request, CancellationToken cancellationToken);
}
