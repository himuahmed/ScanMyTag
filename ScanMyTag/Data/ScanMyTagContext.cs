using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ScanMyTag.Data
{
    public class ScanMyTagContext :DbContext
    {
        public ScanMyTagContext(DbContextOptions<ScanMyTagContext> options) : base(options)
        {

        }

        public DbSet<ContactQR> ContactQr { get; set; }
    }
}
