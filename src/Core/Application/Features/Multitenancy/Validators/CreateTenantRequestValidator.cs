using OpsManagerAPI.Application.Features.Multitenancy.Commands;
using OpsManagerAPI.Application.Features.Multitenancy.Contracts;

namespace OpsManagerAPI.Application.Features.Multitenancy.Validators;

public class CreateDiscoRequestValidator : CustomValidator<CreateDiscoRequest>
{
    public CreateDiscoRequestValidator(
        IDiscoService DiscoService,
        IConnectionStringValidator connectionStringValidator)
    {
        RuleFor(t => t.Id).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MustAsync(async (id, _) => !await DiscoService.ExistsWithIdAsync(id))
                .WithMessage((_, id) => $"Phone number {id} is already registered.");

        RuleFor(t => t.Name).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MustAsync(async (name, _) => !await DiscoService.ExistsWithNameAsync(name!))
                .WithMessage((_, name) => $"Disco {name} already exists.");

        RuleFor(t => t.ConnectionString).Cascade(CascadeMode.Stop)
            .Must((_, cs) => string.IsNullOrWhiteSpace(cs) || connectionStringValidator.TryValidate(cs))
                .WithMessage("Connection string invalid.");

        RuleFor(t => t.AdminEmail).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();
    }
}