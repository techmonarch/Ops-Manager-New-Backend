using OpsManagerAPI.Application.Features.DownloadsManagers.Commands;

namespace OpsManagerAPI.Application.Features.DownloadManagers.Validators;

public class UpdateDocumentAccessibilityCommandRequestValidator : AbstractValidator<UpdateDocumentAccessibilityCommand>
{
    public UpdateDocumentAccessibilityCommandRequestValidator()
    {
        RuleFor(x => x.DocumentId)
     .NotEmpty().WithMessage("Document ID is required.");
        RuleFor(x => x.Roles)
      .NotEmpty().WithMessage("Roles is required.");
    }
}
