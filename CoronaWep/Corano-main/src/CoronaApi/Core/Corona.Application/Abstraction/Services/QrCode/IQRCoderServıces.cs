namespace Corona.Application.Abstraction.Services.QrCode;

public interface IQRCoderServıces
{
    byte[] GenerateQRCode(string text);
}
