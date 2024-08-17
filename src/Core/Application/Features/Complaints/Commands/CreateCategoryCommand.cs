using OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;

namespace OpsManagerAPI.Application.Features.Complaints.Commands;
public class CreateCategoryCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public string Name { get; set; } = default!;
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<ComplaintCategory> _repository;

    public CreateCategoryCommandHandler(IRepositoryWithEvents<ComplaintCategory> repository) => _repository = repository;

    public async Task<ApiResponse<DefaultIdType>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new ComplaintCategory(request.Name);

        await _repository.AddAsync(category, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Complaint Category Successfully Created", category.Id);
    }
}