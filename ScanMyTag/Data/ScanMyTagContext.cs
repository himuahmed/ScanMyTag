using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScanMyTag.Models;

namespace ScanMyTag.Data
{
    public class ScanMyTagContext :IdentityDbContext<UserModel>
    {
        public ScanMyTagContext(DbContextOptions<ScanMyTagContext> options) : base(options)
        {

        }

        public DbSet<ContactQR> ContactQr { get; set; }
    }
}
