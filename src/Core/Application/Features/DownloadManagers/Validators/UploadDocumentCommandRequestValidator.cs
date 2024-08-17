using OpsManagerAPI.Application.Features.DownloadsManagers.Commands;

namespace OpsManagerAPI.Application.Features.DownloadManagers.Validators;

public class UploadDocumentCommandRequestValidator : AbstractValidator<UploadDocumentCommand>
{
    public UploadDocumentCommandRequestValidator()
    {
        RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Description)
          .NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.File)
            .NotEmpty().WithMessage("File is required.");
        RuleFor(x => x.Roles)
            .NotEmpty().WithMessage("Roles is required");
    }
}
