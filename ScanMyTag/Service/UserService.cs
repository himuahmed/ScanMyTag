using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ScanMyTag.Models;

namespace ScanMyTag.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<UserModel> _currentUser;

        public UserService(IHttpContextAccessor httpContext,UserManager<UserModel> currentUser)
        {
            _httpContext = httpContext;
            _currentUser = currentUser;
        }
        public string GetUserId()
        {
          return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public async Task<UserModel> GetCurrentUser()
        {
            return await _currentUser.GetUserAsync(_httpContext.HttpContext.User);
        }
    }
}
