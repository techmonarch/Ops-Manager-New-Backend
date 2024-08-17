using OpsManagerAPI.Application.Features.Complaints.Dtos;
using OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;

namespace OpsManagerAPI.Application.Features.Complaints.Queries;
public interface IComplaintRepository : ITransientService
{
    Task<(List<Complaint> Complaints, int Count)> GetPaginatedAsync(GetComplaintsQuery request, CancellationToken cancellationToken);
    Task<(List<ComplaintCategoryDetailDto> ComplaintCategories, int Count)> GetPaginatedCategoriesAsync(PaginationFilter request, CancellationToken cancellationToken);
    Task<(List<ComplaintSubCategoryDetailDto> ComplaintSubCategories, int Count)> GetPaginatedSubCategoriesAsync(GetComplaintSubCategoriesQuery request, CancellationToken cancellationToken);
}
