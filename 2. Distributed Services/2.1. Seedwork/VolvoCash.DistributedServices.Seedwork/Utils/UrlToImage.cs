using SelectPdf;
using System.Drawing;
using System.IO;

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
            var imgConverter = new HtmlToImage();
            imgConverter.WebPageWidth       = 410;
            imgConverter.WebPageHeight      = 750;
            imgConverter.WebPageFixedSize   = false;
            var image = imgConverter.ConvertUrl(url);
            return image.ImageToByteArray();
        }
    }
}
