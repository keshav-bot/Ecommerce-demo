using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class ForgetPasswordModel
    {

        [Required(ErrorMessage = "Please enter your Email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "please enter valid email address")]
        public string Email { get; set; }
    }
}
