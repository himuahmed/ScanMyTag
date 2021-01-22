using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScanMyTag.Models;

namespace ScanMyTag.Repository
{
    public class AccountRepository : IAccountRepository
    {
        
        private UserManager<UserModel> _identityUserManager;

        public AccountRepository(UserManager<UserModel> identityUserManager)
        {
            _identityUserManager = identityUserManager;
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
    }
}
