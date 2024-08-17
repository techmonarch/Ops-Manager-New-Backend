using OpsManagerAPI.Application.Features.Complaints.Commands;

namespace OpsManagerAPI.Application.Features.Complaints.Validators;

public class CreateSubCategoryCommandRequestValidator : AbstractValidator<CreateSubCategoryCommand>
{
    public CreateSubCategoryCommandRequestValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.CategoryId)
           .NotEmpty().WithMessage("Category ID is required");
    }
}
