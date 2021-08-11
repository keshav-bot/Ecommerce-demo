using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class EditEmployeeViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name of Employee")]
        public String Name { get; set; }
        [Required]
        [Display(Name = "Address of Employee")]
        public String Address { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Employee's Nationality")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Post is required field")]
        [Display(Name="Select the post")]
        public int PostId { get; set; }
        [Display(Name = "Please Upload the Image")]
        public IFormFile EmployeeImage { get; set; }
        public string EmployeeImageUrl { get; set; }
    }
}
