using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScanMyTag.Models;

namespace ScanMyTag.Data
{
    public class ContactQR
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Url { get; set; }
        public string QrCode { get; set; }
        public UserModel User { get; set; }
    }
}
