using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models.ViewModel
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>(); Roles = new List<string>();/// to avoid null reference exception
        }
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
        public List<String> Claims { get; set; }


    }
}
