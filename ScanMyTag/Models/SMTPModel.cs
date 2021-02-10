using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanMyTag.Models
{
    public class SMTPModel
    {
        public string SenderAddress { get; set; }
        public string SenderDisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public bool EnableSSL { get; set; }
        public bool UseDefaultCredential { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
