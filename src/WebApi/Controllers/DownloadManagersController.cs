using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.DownloadsManagers.Commands;
using OpsManagerAPI.Application.Features.DownloadsManagers.Dtos;
using OpsManagerAPI.Application.Features.DownloadsManagers.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;

public class DownloadManagersController : VersionNeutralApiController
{
    [HttpPost("upload-document")]
    [MustHavePermission(OPSAction.Create, OPSResource.DownloadManagers)]
    [OpenApiOperation("Upload a new document.", "Uploads a new document and stores its details.")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<DefaultIdType>> UploadDocument([FromForm] UploadDocumentCommand request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("update-document-accessibility")]
    [MustHavePermission(OPSAction.Update, OPSResource.DownloadManagers)]
    [OpenApiOperation("Update document accessibility.", "Updates the accessibility status of an existing document.")]
    public Task<ApiResponse<DefaultIdType>> UpdateDocumentAccessibility(UpdateDocumentAccessibilityCommand request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("enable-document/{documentId}")]
    [MustHavePermission(OPSAction.Update, OPSResource.DownloadManagers)]
    [OpenApiOperation("Enable a document.", "Enables a previously disabled document.")]
    public Task<ApiResponse<DefaultIdType>> EnableDocument(Guid documentId)
    {
        return Mediator.Send(new EnableDocumentCommand(documentId));
    }

    [HttpPost("disable-document/{documentId}")]
    [MustHavePermission(OPSAction.Update, OPSResource.DownloadManagers)]
    [OpenApiOperation("Disable a document.", "Disables an existing document, making it inaccessible.")]
    public Task<ApiResponse<DefaultIdType>> DisableDocument(Guid documentId)
    {
        return Mediator.Send(new DisableDocumentCommand(documentId));
    }

    [HttpDelete("delete-document/{documentId}")]
    [MustHavePermission(OPSAction.Delete, OPSResource.DownloadManagers)]
    [OpenApiOperation("Delete a document.", "Deletes an existing document and removes its details from the system.")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<DefaultIdType>> DeleteDocument(Guid documentId)
    {
        return Mediator.Send(new DeleteDocumentCommand(documentId));
    }

    [HttpGet]
    [MustHavePermission(OPSAction.View, OPSResource.DownloadManagers)]
    [OpenApiOperation("Get all documents.", "Retrieve a list of all available documents.")]
    public Task<ApiResponse<PaginationResponse<DownloadManagerDto>>> SearchAsync([FromQuery] GetAvailableDocumentsQuery query)
    {
        return Mediator.Send(query);
    }
}
