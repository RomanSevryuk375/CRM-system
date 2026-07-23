using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.Results;
using Microsoft.Extensions.Options;

namespace CRM.Shared.Infrastructure.Data.BlobStorage;

public sealed class MinioFileService(IAmazonS3 s3Client, IOptions<MinioOptions> options) : IFileService
{
    private readonly string _bucketName = options.Value.BucketName;

    public async Task<Result<Stream>> GetFileAsync(string fileName, CancellationToken cancellationToken)
    {
        try
        {
            GetObjectResponse response = await s3Client.GetObjectAsync(_bucketName, fileName, cancellationToken);
            return Result<Stream>.Success(response.ResponseStream);
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return Result<Stream>.Failure(Error.NotFound(
                MinioErrorCodes.NotFound, $"File {fileName} not found."));
        }
        catch (Exception ex)
        {
            return Result<Stream>.Failure(Error.Failure(
                MinioErrorCodes.Error, ex.Message));
        }
    }

    public async Task<Result<string>> UploadFileAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        CancellationToken cancellationToken)
    {
        try
        {
            using TransferUtility transferUtility = new(s3Client);

            TransferUtilityUploadRequest request = new()
            {
                InputStream = fileStream,
                Key = fileName,
                BucketName = _bucketName,
                ContentType = contentType,
                AutoCloseStream = false
            };

            await transferUtility.UploadAsync(request, cancellationToken);

            return Result<string>.Success(fileName);
        }
        catch (Exception ex)
        {
            return Result<string>.Failure(Error.Failure(
                MinioErrorCodes.UploadFailed, ex.Message));
        }
    }

    public async Task<Result> DeleteFileAsync(string fileName, CancellationToken cancellationToken)
    {
        try
        {
            await s3Client.DeleteObjectAsync(_bucketName, fileName, cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(Error.Failure(
                MinioErrorCodes.DeleteFailed, ex.Message));
        }
    }
}