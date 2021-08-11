using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Name Of The Post")]
        public String Name { get; set; }
        [Required]
        [Display(Name ="Salary Of This Post")]
        public double salary { get; set; }
        [Required]
        [Display(Name ="Job Description Of This Post")]
        public String JobDescription { get; set; }
    }
}
