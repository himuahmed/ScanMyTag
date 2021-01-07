using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScanMyTag.Models
{
    public class ContactQRModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter name of your tag.")]
        [Display(Name = "Tag Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your contact details.")]
        [Display(Name = "Contact Details:")]
        public string Contact { get; set; }
        public string Url { get; set; }
        public string QrCode { get; set; }
    }
}
