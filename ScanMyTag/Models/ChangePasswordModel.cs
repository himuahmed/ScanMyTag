using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScanMyTag.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Type your password.")]
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Type your new password.")]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [Compare("ConfirmNewPassword", ErrorMessage = "Password didn't match.")]
        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Confirm your password.")]
        [Display(Name = "Confirm new Password")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

    }
}
