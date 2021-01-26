using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
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
        private readonly IUserService _userService;


        public QRCodeRepository(ScanMyTagContext context, IQRGeneratorService qrGeneratorService, IHttpContextAccessor httpContextAccessor,IUserService userService)
        {
            _context = context;
            _qrGeneratorService = qrGeneratorService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
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
                
                User = await _userService.GetCurrentUser(),
                Contact = contactQrModel.Contact,
                Name = contactQrModel.Name,
                Url = guid.ToString(),
                QrCode = _qrGeneratorService.GenerateQR(baseUrl)
            };

            await _context.ContactQr.AddAsync(newContactQr);
            await _context.SaveChangesAsync();

            return newContactQr.Id;
        }


        public async Task<List<ContactQRModel>> GetAllQrCodes()
        {
            string userId = _userService.GetUserId();

            return await _context.ContactQr.Where(u => u.User.Id == userId).Select(qr => new ContactQRModel()
            {
                User = qr.User,
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
            var userID = _userService.GetUserId();
            var qr = await _context.ContactQr.Where(q => q.Id==id).Select(qm=>new ContactQR()
            {
                User = qm.User,
                Id = qm.Id
            }).FirstOrDefaultAsync();

            if (qr.User.Id == userID)
            {
                _context.ContactQr.Remove(qr);
                int result = await _context.SaveChangesAsync();
                return result;
            }
            else
            {
                return 0;
            }
           
        }

        public async Task<ContactQRModel> GetQrById(int id)
        {
            var qrTag = await _context.ContactQr.Where(x => x.Id == id).Select(qr => new ContactQRModel()
                {
                    User = qr.User,
                    Id = qr.Id,
                    Name = qr.Name,
                    Contact = qr.Contact,
                    QrCode = qr.QrCode,
                    Url = qr.Url
            }).FirstOrDefaultAsync();
            return qrTag;
        }

        public async Task<bool> UpdateQrTag(ContactQRModel contactQr)
        {
            var userID = _userService.GetUserId();

                var qrTag = await GetQrById(contactQr.Id);
                var qrToSave = new ContactQR()
                {
                    Name = contactQr.Name,
                    Id = contactQr.Id,
                    Contact = contactQr.Contact,
                    Url = qrTag.Url,
                    QrCode = qrTag.QrCode
                };
                _context.ContactQr.Update(qrToSave);
                return await _context.SaveChangesAsync() > 0;

        }

    }
}
