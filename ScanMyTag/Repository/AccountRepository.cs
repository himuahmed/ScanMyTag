using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ScanMyTag.Models;
using ScanMyTag.Service;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ScanMyTag.Repository
{
    public class AccountRepository : IAccountRepository
    {
        
        private readonly UserManager<UserModel> _identityUserManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<UserModel> identityUserManager,SignInManager<UserModel> signInManager, IUserService userService,IEmailService emailService,IConfiguration configuration)
        {
            _identityUserManager = identityUserManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
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
            if (result.Succeeded)
            {
                var token = await _identityUserManager.GenerateEmailConfirmationTokenAsync(newUser);
                if (!string.IsNullOrEmpty(token))
                {
                   await SendEmailConfirmation(newUser, token);
                }
            }
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

        public async Task<IdentityResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var userId = _userService.GetUserId();
            var user = await _identityUserManager.FindByIdAsync(userId);

            return await _identityUserManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword,
                changePasswordModel.NewPassword);
        }

        private async Task SendEmailConfirmation(UserModel user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string verificationLink = _configuration.GetSection("Application:EmailVerification").Value;

            EmailOptions emailOptions = new EmailOptions
            {
                EmailReceivers = new List<string>() { user.Email },

                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.Name),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain+verificationLink, user.Id, tok)),
                }
            };
            await _emailService.SendTestEmail(emailOptions);
        }
    }
}
