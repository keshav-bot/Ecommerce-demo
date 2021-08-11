using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class SignInModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter your UserName")]

        [Display(Name ="UserName")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please enter the password")]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }

    }
}
