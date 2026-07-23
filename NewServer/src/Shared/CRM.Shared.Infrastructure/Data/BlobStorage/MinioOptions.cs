namespace CRM.Shared.Infrastructure.Data.BlobStorage;

public sealed record MinioOptions
{
    public const string SectionName = "Minio";
    public string ServiceUrl { get; init; } = string.Empty;
    public string AccessKey { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
    public string BucketName { get; init; } = string.Empty;
}