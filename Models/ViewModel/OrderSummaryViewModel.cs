using KKTraders.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models.ViewModel
{
    public class OrderSummaryViewModel
    {
        public List<Product> ProductList { get; set; }
        public OrderHeader OrderHeader { get; set; }

    }
}
