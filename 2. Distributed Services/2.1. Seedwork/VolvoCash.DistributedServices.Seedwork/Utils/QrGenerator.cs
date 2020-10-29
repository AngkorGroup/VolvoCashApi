using QRCoder;
using System.IO;

namespace VolvoCash.DistributedServices.Seedwork.Utils
{
    public static class QrGenerator
    {
        public static byte[] GenerateQrCode(string content)
        {
            try
            {
                var _qrCode = new QRCodeGenerator();
                QRCodeData _qrCodeData = _qrCode.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(_qrCodeData);
                var qrCodeImage = qrCode.GetGraphic(20);
                var stream = new MemoryStream();
                qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
            catch
            {
                return new byte [0];
            }
        }
    }
}
