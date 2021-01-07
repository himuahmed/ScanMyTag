﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScanMyTag.Repository;
using ScanMyTag.Service;

namespace ScanMyTag.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQRCodeRepository _qrCodeRepository;
        public HomeController(IQRCodeRepository qrCodeRepository)
        {
            _qrCodeRepository = qrCodeRepository;
        }

        public async Task<ViewResult> Index()
        {
            var qrCodes = await _qrCodeRepository.GetAllQrCodes();
            return View(qrCodes);
        }


       
    }
}
