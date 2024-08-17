using OpsManagerAPI.Application.Features.Complaints.Dtos;

namespace OpsManagerAPI.Application.Features.Complaints.Queries;

public class GetComplaintCategoriesQuery : PaginationFilter, IRequest<ApiResponse<PaginationResponse<ComplaintCategoryDetailDto>>>
{
}

public class GetComplaintCategoriesQueryHandler : IRequestHandler<GetComplaintCategoriesQuery, ApiResponse<PaginationResponse<ComplaintCategoryDetailDto>>>
{
    private readonly IComplaintRepository _repository;

    public GetComplaintCategoriesQueryHandler(IComplaintRepository repository) => _repository = repository;

    public async Task<ApiResponse<PaginationResponse<ComplaintCategoryDetailDto>>> Handle(GetComplaintCategoriesQuery request, CancellationToken cancellationToken)
    {
        var (complaintCategories, totalCount) = await _repository.GetPaginatedCategoriesAsync(request, cancellationToken);

        // Create the pagination response
        var paginationResponse = new PaginationResponse<ComplaintCategoryDetailDto>(
            complaintCategories,
            totalCount,
            request.PageNumber,
            request.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<ComplaintCategoryDetailDto>>(true, "Complaint Categories retrieved successfully", paginationResponse);
    }
}