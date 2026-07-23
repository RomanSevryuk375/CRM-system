using CRM.Shared.Abstractions.Results;

namespace CRM.Shared.Abstractions.Abstractions;

public interface IFileService
{
    Task<Result> DeleteFileAsync(string fileName, CancellationToken cancellationToken);
    Task<Result<Stream>> GetFileAsync(string fileName, CancellationToken cancellationToken);
    Task<Result<string>> UploadFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken);
}