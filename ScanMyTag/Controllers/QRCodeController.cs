using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScanMyTag.Data;
using ScanMyTag.Models;
using ScanMyTag.Repository;
using ScanMyTag.Service;

namespace ScanMyTag.Controllers
{
   
    public class QRCodeController : Controller
    {
        private readonly IQRCodeRepository _qrCodeRepository;
        private readonly IUserService _userService;
        
        public QRCodeController(IQRCodeRepository qrCodeRepository,IUserService userService)
        {
            _qrCodeRepository = qrCodeRepository;
            _userService = userService;

        }

        [Route("dashboard")]
        public async Task<ViewResult> DashBoard (bool isSuccess = false)
        {
            var qrCodes = await _qrCodeRepository.GetAllQrCodes();
            if (qrCodes != null)
            {
                return View(qrCodes);
            }
            throw new Exception("Couldn't fetch your tags.");
            
        }

        
        [Route("ContactQR")]
        public IActionResult ContactQRGenerator(string tagName,bool isSuccess = false)
        {
            var model = new ContactQRModel();
            ViewBag.isSuccess = isSuccess;
            ViewBag.tagName = tagName;

            return View(model);
        }



        [HttpPost]
        [Route("ContactQR")]
        public async Task<IActionResult> ContactQRGenerator(ContactQRModel contactQrModel)
        {
            if (ModelState.IsValid)
            {
                int id = await _qrCodeRepository.CreateContactQR(contactQrModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(ContactQRGenerator), new {isSuccess = true, tagName = contactQrModel.Name});
                }

                return View();
            }
            return View();
        }

        [Route("contact-me/{url}", Name = "contact-me")]
        public async Task<ViewResult> GetScannedQr(string url)
        {
            var result = await _qrCodeRepository.GetContactQrByScanning(url);
            return View(result);
        }

        [Route("deleteQr/{id}")]
        public async Task<RedirectToActionResult> DeleteQrById(int id)
        {
           var result = await _qrCodeRepository.DeleteQR(id);
           if (result > 0)
           {
               return RedirectToAction("DashBoard");
           }
           else
           {
               throw new Exception("Couldn't delete your tag.");
           }
        }

        [Route("edittag/{id}")]
        public async Task<IActionResult> EditQr(int id)
        {
            var userId = _userService.GetUserId();
            var qrTag = await _qrCodeRepository.GetQrById(id);
            if (qrTag != null)
            {
                if (qrTag.User.Id == userId)
                {
                    return View(qrTag);
                }
                throw new Exception("Permission Denied.");
            }
            throw new Exception("Couldn't fetch your tag.");
           
        }

        [HttpPost]
        [Route("edittag/{id}")]
        public async Task<IActionResult> EditQr(ContactQRModel contactQr, int Id)
        {
            if (ModelState.IsValid)
            {
                var result = await _qrCodeRepository.UpdateQrTag(contactQr);
                if (result)
                {
                    return RedirectToAction("DashBoard");
                }

                return View();
            }

            return View();
        }


        public async Task<RedirectToActionResult> ChangeQrPrivacy(int Qrid)
        {
            int id = Qrid;
            await _qrCodeRepository.UpdateQrPrivacy(id);
            return RedirectToAction("DashBoard");
        }

    }
}
