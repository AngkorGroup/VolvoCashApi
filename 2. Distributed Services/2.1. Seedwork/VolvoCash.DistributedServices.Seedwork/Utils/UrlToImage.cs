using System.IO;
using SelectPdf;
using System.Drawing;
using System.Drawing.Imaging;

namespace VolvoCash.DistributedServices.Seedwork.Utils
{
    public static class UrlToImage
    {
        public static byte[] ImageToByteArray(this Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }
        public static byte[] DownloadContentAsImage(string url)
        {
            ImageFormat imageFormat = ImageFormat.Jpeg;
            var imgConverter = new HtmlToImage();
            imgConverter.WebPageWidth = 430;
            imgConverter.WebPageHeight = 488;
            var image = imgConverter.ConvertUrl(url);
            return image.ImageToByteArray();
        }
    }
}
