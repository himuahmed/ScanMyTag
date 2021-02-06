using System.Collections.Generic;
using System.Threading.Tasks;
using ScanMyTag.Models;

namespace ScanMyTag.Repository
{
    public interface IQRCodeRepository
    {
        Task<int> CreateContactQR(ContactQRModel contactQrModel);
        Task<List<ContactQRModel>> GetAllQrCodes();
        Task<ContactQRModel> GetContactQrByScanning(string url);
        Task<int> DeleteQR(int id);
        Task<ContactQRModel> GetQrById(int id);
        Task<bool> UpdateQrTag(ContactQRModel contactQr);
        Task<bool> UpdateQrPrivacy(int id);
        Task<bool> CheckQrOwnership(string userId, int qrId);
    }
}