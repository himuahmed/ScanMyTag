using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScanMyTag.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ScanMyTag.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> register(RegistrationModel registrationModel);
        Task<SignInResult> SignIn(SignInModel signInModel);
        Task SignOut();
    }
}