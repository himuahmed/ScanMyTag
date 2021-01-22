using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScanMyTag.Models;

namespace ScanMyTag.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> register(RegistrationModel registrationModel);
    }
}