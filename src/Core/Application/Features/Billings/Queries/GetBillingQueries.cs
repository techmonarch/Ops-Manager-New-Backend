using OpsManagerAPI.Application.Features.Billings.Dtos;

namespace OpsManagerAPI.Application.Features.Billings.Queries;
public class GetBillingQueries : PaginationFilter, IRequest<ApiResponse<PaginationResponse<BillingDetailsDto>>>
{
    public string? CustomerId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GetBillingQueriesHandler : IRequestHandler<GetBillingQueries, ApiResponse<PaginationResponse<BillingDetailsDto>>>
{
    private readonly IBillingRepository _repository;

    public GetBillingQueriesHandler(IBillingRepository repository) => _repository = repository;

    public async Task<ApiResponse<PaginationResponse<BillingDetailsDto>>> Handle(GetBillingQueries request, CancellationToken cancellationToken)
    {
        var (bills, totalCount) = await _repository.GetAll(request, cancellationToken);

        // Create the pagination response
        var paginationResponse = new PaginationResponse<BillingDetailsDto>(
            bills,
            totalCount,
            request.PageNumber,
            request.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<BillingDetailsDto>>(true, "Bills history retrieved successfully", paginationResponse);
    }
}
