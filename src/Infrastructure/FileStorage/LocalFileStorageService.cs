using Microsoft.AspNetCore.Http;
using OpsManagerAPI.Application.Common.FileStorage;
using OpsManagerAPI.Domain.Enums;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace OpsManagerAPI.Infrastructure.FileStorage;
public class LocalFileStorageService : IFileStorageService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LocalFileStorageService(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    public async Task<string> UploadAsync<T>(IFormFile? request, FileType supportedFileType, CancellationToken cancellationToken = default)
        where T : class
    {
        string? fileExtension = Path.GetExtension(request?.FileName).ToLower();

        if (fileExtension is not null && request.Length > 0)
        {
            string folderName = supportedFileType switch
            {
                FileType.Image => Path.Combine("Files", "Images"),
                FileType.Video => Path.Combine("Files", "Videos"),
                _ => Path.Combine("Files", "Others"),
            };

            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            Directory.CreateDirectory(pathToSave);

            string uniqueFileName = GenerateUniqueFileName(fileExtension);
            string fullPath = Path.Combine(pathToSave, uniqueFileName);
            string dbPath = Path.Combine(folderName, uniqueFileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await request.CopyToAsync(stream, cancellationToken);

            var app = _httpContextAccessor.HttpContext.Request;
            string baseUrl = $"{app.Scheme}://{app.Host}{app.PathBase}/";

            return baseUrl + dbPath.Replace("\\", "/");
        }
        else
        {
            return string.Empty;
        }
    }

    public async Task<List<string>> UploadMultipleAsync(List<IFormFile> files, FileType supportedFileType, CancellationToken cancellationToken = default)
    {
        List<string> uploadedFilePaths = new();

        string folderName = supportedFileType switch
        {
            FileType.Image => Path.Combine("Files", "Images"),
            FileType.Video => Path.Combine("Files", "Videos"),
            _ => Path.Combine("Files", "Others"),
        };
        string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        Directory.CreateDirectory(pathToSave);

        foreach (var file in files)
        {
            if (file?.Length > 0)
            {
                string fileExtension = Path.GetExtension(file.FileName).ToLower();

                string uniqueFileName = GenerateUniqueFileName(fileExtension);
                string fullPath = Path.Combine(pathToSave, uniqueFileName);
                string dbPath = Path.Combine(folderName, uniqueFileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(stream, cancellationToken);

                var app = _httpContextAccessor.HttpContext.Request;
                string baseUrl = $"{app.Scheme}://{app.Host}{app.PathBase}/";

                uploadedFilePaths.Add(baseUrl + dbPath.Replace("\\", "/"));
            }
        }

        return uploadedFilePaths;
    }

    public static string RemoveSpecialCharacters(string str)
    {
        return Regex.Replace(str, "[^a-zA-Z0-9_.]+", string.Empty, RegexOptions.Compiled);
    }

    public void Remove(string? path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    private static string GenerateUniqueFileName(string fileExtension)
    {
        byte[] randomBytes = new byte[8];
        RandomNumberGenerator.Fill(randomBytes);
        string uniqueFileName = BitConverter.ToString(randomBytes).Replace("-", string.Empty).ToLower();
        return uniqueFileName + fileExtension;
    }
}
