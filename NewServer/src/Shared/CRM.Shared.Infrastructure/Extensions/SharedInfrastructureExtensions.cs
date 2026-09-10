using Amazon.S3;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Infrastructure.Configuration;
using CRM.Shared.Infrastructure.Data;
using CRM.Shared.Infrastructure.Data.BlobStorage;
using CRM.Shared.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CRM.Shared.Infrastructure.Extensions;

public static class SharedInfrastructureExtensions
{
    public static IServiceCollection AddGlobalSharedInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        config.ExpandEnvironmentVariables();

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.RegisterMyInterceptors();

        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

        IConfigurationSection minioSection = config.GetSection(MinioOptions.SectionName);
        services.Configure<MinioOptions>(minioSection);

        services.AddSingleton<IAmazonS3>(sp =>
        {
            MinioOptions minioOptions = sp.GetRequiredService<IOptions<MinioOptions>>().Value;

            AmazonS3Config s3Config = new()
            {
                ServiceURL = minioOptions.ServiceUrl,
                ForcePathStyle = true
            };

            return new AmazonS3Client(minioOptions.AccessKey, minioOptions.SecretKey, s3Config);
        });

        services.AddSingleton<IFileService, MinioFileService>();

        string connectionString = ConnectionStringHelper.GetConnectionString(config);

        services.AddHealthChecks()
            .AddNpgSql(connectionString)
            .AddS3(options =>
            {
                MinioOptions? minioConfig = config.GetSection(MinioOptions.SectionName).Get<MinioOptions>();
                options.BucketName = minioConfig!.BucketName;
                options.S3Config = new AmazonS3Config
                {
                    ServiceURL = minioConfig.ServiceUrl,
                    ForcePathStyle = true
                };
            }, name: "minio_storage");

        return services;
    }
}