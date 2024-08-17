using OpsManagerAPI.Application.Features.Teams.Commands;

namespace OpsManagerAPI.Application.Features.Teams.Validators;
public class CreateTeamCommandRequestValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandRequestValidator()
    {
        RuleFor(x => x.Name)
       .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Description)
       .NotEmpty().WithMessage("Description is required.");
    }
}
