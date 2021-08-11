using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Data
{
    public class OrderHeader
    {   
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your Phone")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter your Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter your City")]
        public string City { get; set; }
       
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }


    }
}
