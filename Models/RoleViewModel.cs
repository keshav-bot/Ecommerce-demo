using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace KKTraders.Models
{
    public class RoleViewModel
    {
        public string  Id { get; set; }
        [Required]
        
        public string Name { get; set; }
       public List<string> UserRole { get; set; }

    }
}
