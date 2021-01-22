using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScanMyTag.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ScanMyTag.Repository
{
    public class AccountRepository : IAccountRepository
    {
        
        private readonly UserManager<UserModel> _identityUserManager;
        private readonly SignInManager<UserModel> _signInManager;

        public AccountRepository(UserManager<UserModel> identityUserManager,SignInManager<UserModel> signInManager)
        {
            _identityUserManager = identityUserManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> register(RegistrationModel registrationModel)
        {
            var newUser = new UserModel()
            {
                Email = registrationModel.Email,
                UserName = registrationModel.Email,
                Address = registrationModel.Address,
                BirthDate = registrationModel.BirthDate,
                Name = registrationModel.Name
            };
            var result = await _identityUserManager.CreateAsync(newUser, registrationModel.Password);
            return result;

        }

        public async Task<SignInResult> SignIn(SignInModel signInModel)
        {
            return await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password,
                signInModel.RememberMe, false);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
