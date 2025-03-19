using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;

namespace CreatorFund.Application.Services;

public class AwsTest
{
    private readonly IAmazonS3 _s3Client;
    private readonly string? _bucketName;

    public AwsTest(IAmazonS3 s3Client,IOptions<AWSResources> awsResourcesOptions)
    {
        _s3Client = s3Client;
        _bucketName = awsResourcesOptions.Value.BucketName;
    }

    public async Task PutObject()
    {
        await using var inputStream = new FileStream("./movies.csv", FileMode.Open, FileAccess.Read);

        var putObjectRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = "files/movies.csv",
            ContentType = "text/csv",
            InputStream = inputStream
        };

        await _s3Client.PutObjectAsync(putObjectRequest);
    }
}
