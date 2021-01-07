using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QRCoder;

namespace ScanMyTag.Service
{
    public class QRGeneratorService : IQRGeneratorService
    {
        public string GenerateQR(string qrText)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                using (Bitmap bitmap = qrCode.GetGraphic(20))
                {
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    var qrImage = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                    return qrImage;
                }
            }
        }
    }
}
