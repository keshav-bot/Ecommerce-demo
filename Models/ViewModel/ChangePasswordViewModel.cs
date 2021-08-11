using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace KKTraders.Models.ViewModel
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter your password"), DataType(DataType.Password)]
        [Display(Name ="Your Current password")]
        public string  CurrentPassword { get; set; }
        [Required(ErrorMessage = "Please enter your New  password"), DataType(DataType.Password)]
        [Display(Name = "Your Current New Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please enter your ConfirmPassword"), DataType(DataType.Password)]
        [Display(Name = "Your Current ConfirmPassword")]
        [Compare("NewPassword",ErrorMessage ="New Password and Conifrm New password do not match")]
        public string ConfirmNewPassword { get; set; }
    }
}
