namespace CRMSystem.Business.Abstractions;

public interface IFileService
{
    Task<string> UploadFile(Stream fileStrim, string fileName, string contentType, CancellationToken ct);
    Task<Stream> GetFile(string fileName, CancellationToken ct);
    Task DeleteFile(string fileName, CancellationToken ct);
}
