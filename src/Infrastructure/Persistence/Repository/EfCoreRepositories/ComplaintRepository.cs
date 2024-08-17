using Ardalis.Specification.EntityFrameworkCore;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Application.Features.Complaints.Dtos;
using OpsManagerAPI.Application.Features.Complaints.Queries;
using OpsManagerAPI.Application.Features.Complaints.Queries.QueryBuilder;
using OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;
public class ComplaintRepository : IComplaintRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ComplaintRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(List<Complaint> Complaints, int Count)> GetPaginatedAsync(GetComplaintsQuery request, CancellationToken cancellationToken)
    {
        // Build the filter expression
        var spec = new EntitiesByPaginationFilterSpec<Complaint>(request);
        var filter = ComplaintQueryBuilder.BuildFilter(request);

        // Calculate the total count of complaints matching the filter
        int totalCount = await _dbContext.Complaints
                                         .AsNoTracking()
                                         .CountAsync(cancellationToken: cancellationToken);

        // Fetch the paginated complaints including the related entities
        var complaints = await _dbContext.Complaints
                                         .AsNoTracking()
                                         .Include(c => c.Customer)
                                         .ThenInclude(x => x.DistributionTransformer)
                                         .ThenInclude(x => x.Store)
                                         .Include(c => c.Category)
                                         .Include(c => c.SubCategory)
                                         .Where(filter)
                                         .WithSpecification(spec)
                                         .ToListAsync(cancellationToken: cancellationToken);

        return (complaints, totalCount);
    }

    public async Task<(List<ComplaintCategoryDetailDto> ComplaintCategories, int Count)> GetPaginatedCategoriesAsync(PaginationFilter request, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpec<ComplaintCategory>(request);

        int totalCount = await _dbContext.ComplaintCategories
                                         .AsNoTracking()
                                         .CountAsync(cancellationToken: cancellationToken);

        var complaintCategories = (await _dbContext.ComplaintCategories
                                         .AsNoTracking()
                                         .WithSpecification(spec)
                                         .ToListAsync(cancellationToken: cancellationToken))
                                         .Adapt<List<ComplaintCategoryDetailDto>>();

        return (complaintCategories, totalCount);
    }

    public async Task<(List<ComplaintSubCategoryDetailDto> ComplaintSubCategories, int Count)> GetPaginatedSubCategoriesAsync(GetComplaintSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpec<ComplaintSubCategory>(request);

        int totalCount = await _dbContext.ComplaintSubCategories
                                         .AsNoTracking()
                                         .CountAsync(cancellationToken: cancellationToken);

        var complaintSubCategoriesQuery = _dbContext.ComplaintSubCategories
                                             .AsNoTracking();

        // Check if CategoryId is present; if not, return all subcategories
        if (request.CategoryId.HasValue)
        {
            complaintSubCategoriesQuery = complaintSubCategoriesQuery
                                            .Where(c => c.CategoryId == request.CategoryId.Value);
        }

        var complaintSubCategories = (await complaintSubCategoriesQuery
                                          .WithSpecification(spec)
                                         .ToListAsync(cancellationToken: cancellationToken))
                                         .Adapt<List<ComplaintSubCategoryDetailDto>>();

        return (complaintSubCategories, totalCount);
    }
}
