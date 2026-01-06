namespace CRMSystem.Business.Abstractions;

public interface IFileService
{
    Task<string> UploadFile(Stream fileStrim, string fileName, string contentType);
    Task<Stream> GetFile(string fileName);
    Task DeleteFile(string fileName);
}
