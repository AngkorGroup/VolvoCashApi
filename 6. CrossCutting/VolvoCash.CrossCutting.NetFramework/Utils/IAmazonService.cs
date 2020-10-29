using System.IO;
using System.Threading.Tasks;

namespace VolvoCash.CrossCutting.NetFramework.Utils
{
    public interface IAmazonBucketService
    {
        Task<string> UploadImageUrlToS3(string url, string fileExtension, string prefixPath = "");
        Task<string> UploadStreamToS3(Stream streamFileToUpload, string fileExtension, string prefixPath = "");
    }
}
