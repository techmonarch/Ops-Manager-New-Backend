using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.Customers.Dtos;
using OpsManagerAPI.Application.Features.Customers.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;

public class CustomersController : VersionNeutralApiController
{
    private readonly ICustomerQueries _customerQueries;

    public CustomersController(ICustomerQueries customerQueries) => _customerQueries = customerQueries;

    /// <summary>
    /// Get a customer's details by ID.
    /// </summary>
    /// <param name="id">The ID of the customer.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A customer's details.</returns>
    [NonAction]
    [HttpGet("id/{id}")]
    [MustHavePermission(OPSAction.Search, OPSResource.Customers)]
    [OpenApiOperation("Get a customer's details by ID.", "Retrieve a customer's details using their unique identifier.")]
    public Task<ApiResponse<CustomerDetailDto>> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return _customerQueries.GetAsync(id, cancellationToken);
    }

    /// <summary>
    /// Get a customer's details by account number.
    /// </summary>
    /// <param name="accountNumber">The account number of the customer.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A customer's details.</returns>
    [NonAction]
    [HttpGet("account-number/{accountNumber}")]
    [MustHavePermission(OPSAction.Search, OPSResource.Customers)]
    [OpenApiOperation("Get a customer's details by account number.", "Retrieve a customer's details using their account number.")]
    public Task<ApiResponse<CustomerDetailDto>> GetByAccountNumberAsync(string accountNumber, CancellationToken cancellationToken)
    {
        return _customerQueries.GetByAccountNumberAsync(accountNumber, cancellationToken);
    }

    /// <summary>
    /// Get customer data.
    /// </summary>
    /// <param name="request">The customer filter request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of customer data.</returns>
    [HttpGet]
    [MustHavePermission(OPSAction.View, OPSResource.Customers)]
    [OpenApiOperation("Get customer data.", "Retrieve a paginated list of customer data with detailed information.")]
    public async Task<ApiResponse<PaginationResponse<CustomerDataDto>>> SearchAsync([FromQuery] CustomerFilterRequest request, CancellationToken cancellationToken)
    {
        return await _customerQueries.SearchAsync(request, cancellationToken);
    }

    /// <summary>
    /// Get customer data.
    /// </summary>
    /// <param name="request">The customer filter request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of customer data.</returns>
    [HttpGet("bill-distribution-and-meter-reading-customers")]
    [MustHavePermission(OPSAction.View, OPSResource.Customers)]
    [OpenApiOperation("Get customer data.", "Retrieve a paginated list of customer data with detailed information.")]
    public async Task<ApiResponse<PaginationResponse<MeterReadingsAndBillDistributionsCustomerDataDto>>> GetForMeterReadingsAndBillDistributions([FromQuery] CustomerFilterRequest request, CancellationToken cancellationToken)
    {
        return await _customerQueries.GetForMeterReadingsAndBillDistributions(request, cancellationToken);
    }

    /// <summary>
    /// Get customer data.
    /// </summary>
    /// <param name="request">The customer filter request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of customer data.</returns>
    [HttpGet("evaluation-and-enumeration-customers")]
    [MustHavePermission(OPSAction.View, OPSResource.Customers)]
    [OpenApiOperation("Get customer data.", "Retrieve a paginated list of customer data with detailed information.")]
    public async Task<ApiResponse<PaginationResponse<EvaluationsAndEnumerationsCustomerDataDto>>> GetForEvaluationsAndEnumerations([FromQuery] CustomerFilterRequest request, CancellationToken cancellationToken)
    {
        return await _customerQueries.GetForEvaluationsAndEnumerations(request, cancellationToken);
    }
}
