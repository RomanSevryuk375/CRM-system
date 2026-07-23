namespace CRM.Shared.Infrastructure.Data.BlobStorage;

public static class MinioErrorCodes
{
    public const string NotFound = "File.NotFound";
    public const string Error = "File.Error";
    public const string UploadFailed = "File.UploadFailed";
    public const string DeleteFailed = "File.DeleteFailed";
}
