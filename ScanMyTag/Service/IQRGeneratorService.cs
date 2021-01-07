namespace ScanMyTag.Service
{
    public interface IQRGeneratorService
    {
        string GenerateQR(string qrText);
    }
}