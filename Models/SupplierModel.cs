using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KKTraders.Data;

namespace KKTraders.Models
{
    public class SupplierModel
    {
      public  IList<Supplier> supplier { get; set; }
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

        public string PhoneNumber { get; set; }
        [Required]

        public byte status { get; set; }

    }
}
