using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class Cart
    {
        public ICollection<ProductModel> Product { get; set; }
        public OrderHeaderModel OrderHeader { get; set; }
    }
}
