using Corona.Application.Abstraction.Services.QrCode;
using QRCoder;

namespace Corona.Infrastructure.Services.QrCode;

public class QRCoderServıces : IQRCoderServıces
{
    public byte[] GenerateQRCode(string text)
    {
        QRCodeGenerator generator = new();
        QRCodeData data = generator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        using PngByteQRCode qRCode = new(data);
        byte[] byteGraphic = qRCode.GetGraphic(10, new byte[] { 84, 99, 71 }, new byte[] { 240, 240, 240 });
        return byteGraphic;
    }
}
