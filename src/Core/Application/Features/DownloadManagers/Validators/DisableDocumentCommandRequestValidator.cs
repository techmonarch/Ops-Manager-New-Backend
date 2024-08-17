using OpsManagerAPI.Application.Features.DownloadsManagers.Commands;

namespace OpsManagerAPI.Application.Features.DownloadManagers.Validators;

public class DisableDocumentCommandRequestValidator : AbstractValidator<DisableDocumentCommand>
{
    public DisableDocumentCommandRequestValidator()
    {
        RuleFor(x => x.DocumentId)
          .NotEmpty().WithMessage("Document ID is required.");
    }
}
