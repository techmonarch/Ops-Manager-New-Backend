using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Domain.Enums;

namespace OpsManagerAPI.Application.Common.FileStorage;

public interface IFileStorageService : ITransientService
{
    public Task<string> UploadAsync<T>(IFormFile? request, FileType supportedFileType, CancellationToken cancellationToken = default)
    where T : class;
    Task<List<string>> UploadMultipleAsync(List<IFormFile> files, FileType supportedFileType, CancellationToken cancellationToken = default);
    public void Remove(string? path);
}