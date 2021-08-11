using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class resetPasswordModel
    {
        [Required(ErrorMessage = "Please enter your Email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "please enter valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Field is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword Field is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Token { get; set; }
        
       
    }
}
