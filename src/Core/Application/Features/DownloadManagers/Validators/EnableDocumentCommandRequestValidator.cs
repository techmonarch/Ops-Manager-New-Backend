using OpsManagerAPI.Application.Features.DownloadsManagers.Commands;

namespace OpsManagerAPI.Application.Features.DownloadManagers.Validators;

public class EnableDocumentCommandRequestValidator : AbstractValidator<EnableDocumentCommand>
{
    public EnableDocumentCommandRequestValidator()
    {
        RuleFor(x => x.DocumentId)
          .NotEmpty().WithMessage("Document ID is required.");
    }
}
