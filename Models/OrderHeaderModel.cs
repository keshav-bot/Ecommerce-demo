using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class OrderHeaderModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(15,ErrorMessage ="Length should be less than 15 characte")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Phone Number  is Required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is Required")]
        public string City { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
    }
}
