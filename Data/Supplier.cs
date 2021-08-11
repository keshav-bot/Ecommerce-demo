using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace KKTraders.Data
{
    public class Supplier
    {
        
        public int Id { get; set; }
        [Required]
        [StringLength(20)]

        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Address { get; set; }
        [Required]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]

        public string  PhoneNumber { get; set; }
        [Required]

        public byte status { get; set; }

    }
}
