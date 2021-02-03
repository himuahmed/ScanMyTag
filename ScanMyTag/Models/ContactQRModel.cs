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
        //[MaxLength(16,ErrorMessage = "Name can not exceeds 16 character.")]
        [MinLength(1, ErrorMessage = "Enter a tag name.")]
        [Display(Name = "Tag Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your contact details.")]
        [Display(Name = "Your Text:")]
        public string Contact { get; set; }
        public string Url { get; set; }
        public string QrCode { get; set; }

        public bool Enabled { get; set; }
        public UserModel User { get; set; }

    }
}
