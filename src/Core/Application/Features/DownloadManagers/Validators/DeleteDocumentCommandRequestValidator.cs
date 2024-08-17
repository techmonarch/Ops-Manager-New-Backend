using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Application.Features.Disconnections.Commands;
using OpsManagerAPI.Application.Features.DownloadsManagers.Commands;

namespace OpsManagerAPI.Application.Features.DownloadManagers.Validators;
public class DeleteDocumentCommandRequestValidator : AbstractValidator<DeleteDocumentCommand>
{
    public DeleteDocumentCommandRequestValidator()
    {
        RuleFor(x => x.DocumentId)
           .NotEmpty().WithMessage("Document ID is required.");
    }
}
