using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public class AmazonBucketService : IAmazonBucketService
    {
        #region Members
        private readonly IConfiguration _configuration;
        private readonly ILogger<AmazonBucketService> _logger;
        #endregion
        
        #region Constructor
        public AmazonBucketService(IConfiguration configuration, ILogger<AmazonBucketService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        #endregion
        
        #region Public Methods
        public async Task<string> UploadImageUrlToS3(string url, string fileExtension, string prefixPath = "")
        {
            var s3Url = "";
            using (var client = new WebClient())
            {
                var content = client.DownloadData(url);
                using (var stream = new MemoryStream(content))
                {
                    s3Url =  await UploadStreamToS3(stream, fileExtension, prefixPath);
                }
            }
            return s3Url;
        }

        public async Task<string> UploadStreamToS3(Stream streamFileToUpload, string fileExtension, string prefixPath = "")
        {
            try
            {
                var bucketPath = "";
                var accessKeyId = _configuration["AWS:S3:AccessKeyId"];
                var secretKey = _configuration["AWS:S3:SecretKey"];
                var region = _configuration["AWS:S3:Region"];
                var bucketName = _configuration["AWS:S3:BucketName"];
                using (var client = new AmazonS3Client(accessKeyId, secretKey, RegionEndpoint.USEast2))
                {
                    using (var newMemoryStream = new MemoryStream())
                    {
                        streamFileToUpload.CopyTo(newMemoryStream);
                        var extension = fileExtension;
                        var keyName = (string.IsNullOrEmpty(prefixPath) ? "" : prefixPath + (prefixPath.EndsWith("/") ? "" : "/")) + Guid.NewGuid() + extension;
                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = newMemoryStream,
                            Key = keyName,
                            BucketName = bucketName,
                            CannedACL = S3CannedACL.PublicRead
                        };
                        var fileTransferUtility = new TransferUtility(client);
                        await fileTransferUtility.UploadAsync(uploadRequest);
                        bucketPath = _configuration["AWS:S3:BucketUrl"] + keyName;
                    }
                }
                return bucketPath;
            }
            catch(Exception e)
            {
                _logger.LogError($"Exception while uploading file to S3 => "+ e.Message);
            }
            return "";
        }
        #endregion
    }
}

