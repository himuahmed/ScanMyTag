using System.Collections.Generic;
using System.Threading.Tasks;
using ScanMyTag.Models;

namespace ScanMyTag.Repository
{
    public interface IQRCodeRepository
    {
        Task<int> CreateContactQR(ContactQRModel contactQrModel);
        Task<List<QRModel>> GetAllQrCodes();
        Task<ContactQRModel> GetContactQrByScanning(string url);
    }
}