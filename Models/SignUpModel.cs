using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class SignUpModel
    {
        public int  Id { get; set; }
        [Required(ErrorMessage ="Please Enter Your  email")]
        [Display(Name ="Please Enter Your UserName")]
        [EmailAddress(ErrorMessage ="Please provide valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please Enter your Passwowrd")]
        [Display(Name = "Please Enter Passsword")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword",ErrorMessage ="Password and confirm password do not match")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Please enter your Confirm password")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Please Enter your Name")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

    }

}
