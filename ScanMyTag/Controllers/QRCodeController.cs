using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScanMyTag.Models;
using ScanMyTag.Repository;
using ScanMyTag.Service;

namespace ScanMyTag.Controllers
{
   
    public class QRCodeController : Controller
    {
        private readonly IQRCodeRepository _qrCodeRepository;
        
        public QRCodeController(IQRCodeRepository qrCodeRepository)
        {
            _qrCodeRepository = qrCodeRepository;
            
        }

        [Route("dashboard")]
        public async Task<ViewResult> DashBoard (bool isSuccess = false)
        {
            var qrCodes = await _qrCodeRepository.GetAllQrCodes();
            return View(qrCodes);
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
               return RedirectToAction("DashBoard");
           }
        }

    }
}
