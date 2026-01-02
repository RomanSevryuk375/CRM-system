using Amazon.S3;
using Amazon.S3.Transfer;
using CRMSystem.Buisness.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisness.Services;

public class MinioFileService : IFileService
{
    private readonly ILogger<MinioFileService> _logger;
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public MinioFileService(
        IConfiguration configuration, 
        ILogger<MinioFileService> logger)
    {
        _logger = logger;

        var config = configuration.GetSection("Minio");
        _bucketName = config["BucketName"]!;

        var s3Config = new AmazonS3Config
        {
            ServiceURL = config["ServiceUrl"],
            ForcePathStyle = true
        };

        _s3Client = new AmazonS3Client(
            config["AccessKey"],
            config["SecretKey"],
            s3Config);
    }
    public async Task DeleteFile(string fileName)
    {
        await _s3Client.DeleteObjectAsync(_bucketName, fileName);
        _logger.LogInformation("File {FileName} deleted from MinIO", fileName);
    }

    public async Task<Stream> GetFile(string fileName)
    {
        try
        {
            var response = await _s3Client.GetObjectAsync(_bucketName, fileName);

            return response.ResponseStream;
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogError("File {FileName} not found in MinIO", fileName);
            throw new FileNotFoundException($"File {fileName} not found");
        }
    }

    public async Task<string> UploadFile(Stream fileStrim, string fileName, string contentType)
    {
        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";

        try
        {
            var transferUtility = new TransferUtility(_s3Client);

            await transferUtility.UploadAsync(new TransferUtilityUploadRequest
            {
                InputStream = fileStrim,
                Key = uniqueFileName,
                BucketName = _bucketName,
                ContentType = contentType,
                AutoCloseStream = false
            });

            _logger.LogInformation("File {FileName} uploaded to MinIO", uniqueFileName);

            return uniqueFileName;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "MinIO upload failed");
            throw;
        }
    }
}
