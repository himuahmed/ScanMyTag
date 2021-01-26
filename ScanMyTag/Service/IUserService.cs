using System.Threading.Tasks;
using ScanMyTag.Models;

namespace ScanMyTag.Service
{
    public interface IUserService
    {
        string GetUserId();
        Task<UserModel> GetCurrentUser();
    }
}