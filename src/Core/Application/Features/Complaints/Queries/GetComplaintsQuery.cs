using OpsManagerAPI.Application.Features.Complaints.Dtos;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Features.Complaints.Queries;

public class GetComplaintsQuery : PaginationFilter, IRequest<ApiResponse<PaginationResponse<ComplaintDetailDto>>>
{
    public DefaultIdType? Id { get; set; }
    public ComplaintType? ComplaintType { get; set; }
}

public class GetComplaintsQueryHandler : IRequestHandler<GetComplaintsQuery, ApiResponse<PaginationResponse<ComplaintDetailDto>>>
{
    private readonly IComplaintRepository _repository;

    public GetComplaintsQueryHandler(IComplaintRepository repository) => _repository = repository;

    public async Task<ApiResponse<PaginationResponse<ComplaintDetailDto>>> Handle(GetComplaintsQuery request, CancellationToken cancellationToken)
    {
        var (complaints, totalCount) = await _repository.GetPaginatedAsync(request, cancellationToken);

        var complaintDataList = complaints.ConvertAll(complaint =>
        {
            return new ComplaintDetailDto
            {
                Category = complaint.Category?.Name,
                SubCategory = complaint.SubCategory?.Name,
                Comment = complaint.Comment,
                ImagePath = complaint.ImagePath,
                CustomerAccountNumber = complaint.Customer?.AccountNumber,
                Id = complaint.Id,
                CustomerName = complaint.Customer?.FirstName,
                CustomerPhone = complaint.Customer?.Phone,
                DistributionTransformerName = complaint?.DistributionTransformer?.Name,
                CustomerAdddress = complaint.Customer?.DistributionTransformer?.Store?.Name
            };
        });

        // Create the pagination response
        var paginationResponse = new PaginationResponse<ComplaintDetailDto>(
            complaintDataList,
            totalCount,
            request.PageNumber,
            request.PageSize);

        // Return the API response
        return new ApiResponse<PaginationResponse<ComplaintDetailDto>>(true, "Complaints retrieved successfully", paginationResponse);
    }
}