using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models.ViewModel
{
    public class OrderHeaderViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }

        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }
        public string ProductName { get; set; }
    }
}
