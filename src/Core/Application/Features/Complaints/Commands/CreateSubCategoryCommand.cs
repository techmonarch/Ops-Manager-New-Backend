using OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;

namespace OpsManagerAPI.Application.Features.Complaints.Commands;

public class CreateSubCategoryCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public string Name { get; set; } = default!;
    public Guid CategoryId { get; set; }
}

public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<ComplaintSubCategory> _repository;

    public CreateSubCategoryCommandHandler(IRepositoryWithEvents<ComplaintSubCategory> repository) => _repository = repository;

    public async Task<ApiResponse<DefaultIdType>> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new ComplaintSubCategory(request.Name, request.CategoryId);

        await _repository.AddAsync(category, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Complaint Sub-Category Successfully Created", category.Id);
    }
}

