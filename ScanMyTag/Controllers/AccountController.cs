using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScanMyTag.Models;
using ScanMyTag.Repository;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ScanMyTag.Controllers
{
    public class AccountController : Controller
    {
        private IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [Route("register")]
        public IActionResult Register(bool isSuccess = false)
        {
            ViewBag.isSuccess = isSuccess;
            return View();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
            if (ModelState.IsValid)
            {
              var result = await _accountRepository.register(registrationModel);
              if (!result.Succeeded)
              {
                  foreach (var err in result.Errors)
                  {
                      ModelState.AddModelError("", err.Description);
                  }

                  return View(registrationModel);
              }
              ModelState.Clear();
              return RedirectToAction(nameof(Register), new {isSuccess = true});
              
            }
            return View();
        }

        [Route("login")]
        public IActionResult SignIn()
        {
            return View();
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> SignIn(SignInModel signInModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.SignIn(signInModel);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error signing in");
                }
            }
            return View(signInModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await _accountRepository.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Route("change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePassword(changePasswordModel);

                if (result.Succeeded)
                {
                    ModelState.Clear();
                    return RedirectToAction("DashBoard", "QRCode");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }

            }

            return View();
        }

    }
}
