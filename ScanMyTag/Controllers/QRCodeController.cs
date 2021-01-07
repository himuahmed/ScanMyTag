using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [Route("ContactQR")]
        public IActionResult ContactQRGenerator(bool isSuccess = false)
        {
            var model = new ContactQRModel();
            ViewBag.isSuccess = isSuccess;
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
                    return RedirectToAction(nameof(ContactQRGenerator), new {isSuccess = true});
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

    }
}
