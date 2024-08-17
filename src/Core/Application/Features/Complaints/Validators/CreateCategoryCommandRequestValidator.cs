using OpsManagerAPI.Application.Features.Complaints.Commands;

namespace OpsManagerAPI.Application.Features.Complaints.Validators;

public class CreateCategoryCommandRequestValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandRequestValidator()
    {
         RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}
