using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class FrequencModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Product That you Sale on like kG Dorzon etc")]
        public string Name { get; set; }
        [Required]
   
        public int FrequencyCount{ get; set; }
    }
}
