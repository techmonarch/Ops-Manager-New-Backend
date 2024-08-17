using OpsManagerAPI.Application.Features.Complaints.Dtos;

namespace OpsManagerAPI.Application.Features.Complaints.Queries;

public class GetComplaintSubCategoriesQuery : PaginationFilter, IRequest<ApiResponse<PaginationResponse<ComplaintSubCategoryDetailDto>>>
{
    public DefaultIdType? CategoryId { get; set; }
}

public class GetComplaintSubCategoriesQueryHandler : IRequestHandler<GetComplaintSubCategoriesQuery, ApiResponse<PaginationResponse<ComplaintSubCategoryDetailDto>>>
{
    private readonly IComplaintRepository _repository;

    public GetComplaintSubCategoriesQueryHandler(IComplaintRepository repository) => _repository = repository;
    public async Task<ApiResponse<PaginationResponse<ComplaintSubCategoryDetailDto>>> Handle(GetComplaintSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        var (complaintSubCategories, totalCount) = await _repository.GetPaginatedSubCategoriesAsync(request, cancellationToken);

        // Create the pagination response
        var paginationResponse = new PaginationResponse<ComplaintSubCategoryDetailDto>(
            complaintSubCategories,
            totalCount,
            request.PageNumber,
            request.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<ComplaintSubCategoryDetailDto>>(true, "Complaint Sub-Categories retrieved successfully", paginationResponse);
    }
}
