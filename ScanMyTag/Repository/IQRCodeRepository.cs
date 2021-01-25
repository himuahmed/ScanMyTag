using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using ScanMyTag.Data;
using ScanMyTag.Models;

namespace ScanMyTag.Repository
{
    public interface IQRCodeRepository
    {
        string baseUrl();
        Task<int> CreateContactQR(ContactQRModel contactQrModel);
        Task<List<QRModel>> GetAllQrCodes();
        Task<ContactQRModel> GetContactQrByScanning(string url);
        Task<int> DeleteQR(int id);
        Task<ContactQR> GetQrById(int id);
        Task<bool> UpdateQrTag(ContactQR contactQr);
    }
}