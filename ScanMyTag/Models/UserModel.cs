using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ScanMyTag.Models
{
    public class UserModel :IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }

    }
}
