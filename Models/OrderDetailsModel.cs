using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class OrderDetailsModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Name Field is Required")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please Enter your Valid Email")]
        public String Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }
        [Required]
        public String Province { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string Municipaility { get; set; }
        [Required]
        public int WardNo { get; set; }
        public int OrderHeaderId { get; set; }


    }
}
