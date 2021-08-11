using KKTraders.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace KKTraders.Areas.Admin.Models
{
    public class AddQuantity
    {
        
        [Required(ErrorMessage ="Quantity is Required")]
        public double Quantity { get; set; }
        [Required(ErrorMessage ="Please  select the item")]
        public int ProductId { get; set; }
        public string  ProductName { get; set; }
        public IList<Product> Product { get; set; }
        public bool Updated { get; set; }
    }
}
