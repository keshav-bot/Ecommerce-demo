using KKTraders.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models.ViewModel
{
    public class OrderHeaderOrderDetailsViewModel
    {   
        public IList<OrderDetails> orderDetails { get; set; }
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
