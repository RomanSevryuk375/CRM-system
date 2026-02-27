// Ignore Spelling: Minio

using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using CRMSystem.Business.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

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
    
    private async Task EnsureBucketExists(CancellationToken ct)
    {
        try 
        {
            var exists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _bucketName);
            
            if (!exists)
            {
                _logger.LogInformation("Bucket {BucketName} not found. Creating...", _bucketName);
                
                var putBucketRequest = new PutBucketRequest
                {
                    BucketName = _bucketName,
                    UseClientRegion = true
                };

                await _s3Client.PutBucketAsync(putBucketRequest, ct);
                
                _logger.LogInformation("Bucket {BucketName} created successfully.", _bucketName);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking/creating bucket {BucketName}", _bucketName);
            throw;
        }
    }
    
    public async Task DeleteFile(string fileName, CancellationToken ct)
    {
        await _s3Client.DeleteObjectAsync(_bucketName, fileName, ct);
        _logger.LogInformation("File {FileName} deleted from MinIO", fileName);
    }

    public async Task<Stream> GetFile(string fileName, CancellationToken ct)
    {
        try
        {
            var response = await _s3Client.GetObjectAsync(_bucketName, fileName, ct);

            return response.ResponseStream;
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogError("File {FileName} not found in MinIO", fileName);
            throw new FileNotFoundException($"File {fileName} not found");
        }
    }

    public async Task<string> UploadFile(
        Stream fileStream, string fileName, string contentType, CancellationToken ct)
    {
        await EnsureBucketExists(ct);
        
        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";

        try
        {
            var transferUtility = new TransferUtility(_s3Client);

            await transferUtility.UploadAsync(new TransferUtilityUploadRequest
            {
                InputStream = fileStream,
                Key = uniqueFileName,
                BucketName = _bucketName,
                ContentType = contentType,
                AutoCloseStream = false,
            }, ct);

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
