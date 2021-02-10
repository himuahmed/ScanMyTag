using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScanMyTag.Models
{
    public class EmailOptions
    {
        public List<string> EmailReceivers { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
