using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage ="Enter your firstName")]
        
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}
