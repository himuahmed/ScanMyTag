using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScanMyTag.Data;
using ScanMyTag.Models;
using ScanMyTag.Service;


namespace ScanMyTag.Repository
{
    public class QRCodeRepository : IQRCodeRepository
    {
        private readonly ScanMyTagContext _context = null;
        private readonly IQRGeneratorService _qrGeneratorService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public QRCodeRepository(ScanMyTagContext context, IQRGeneratorService qrGeneratorService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _qrGeneratorService = qrGeneratorService;
            _httpContextAccessor = httpContextAccessor;
        }

        public string baseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            string baseUrl = $"{request.Scheme}://{request.Host.Value.ToString()}{request.PathBase.Value.ToString()}";
            return baseUrl;
        }
        public async Task<int> CreateContactQR(ContactQRModel contactQrModel)
        {
            string domainUrl = this.baseUrl();
            Guid guid = Guid.NewGuid();
            string baseUrl = domainUrl + "/contact-me/" + guid.ToString();
            var newContactQr = new ContactQR()
            {
                Contact = contactQrModel.Contact,
                Name = contactQrModel.Name,
                Url = guid.ToString(),
                QrCode = _qrGeneratorService.GenerateQR(baseUrl)
            };

            await _context.ContactQr.AddAsync(newContactQr);
            await _context.SaveChangesAsync();

            return newContactQr.Id;
        }


        public async Task<List<QRModel>> GetAllQrCodes()
        {
            return await _context.ContactQr.Select(qr => new QRModel()
            {
                Id = qr.Id,
                Name = qr.Name,
                Url = qr.Url,
                QrCode = qr.QrCode
            }).ToListAsync();
        }

        public async Task<ContactQRModel> GetContactQrByScanning(string url)
        {
            var result= await _context.ContactQr.Where(qr => qr.Url == url).Select(qr => new ContactQRModel()
            {
                Id = qr.Id,
                Name = qr.Name,
                Contact = qr.Contact,
                QrCode = qr.QrCode,
                Url = qr.Url
            }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> DeleteQR(int id)
        {
            var qr = new ContactQR(){ Id = id};
            _context.Remove(qr); 
            int result = await _context.SaveChangesAsync();
            return result;
        }

    }
}
