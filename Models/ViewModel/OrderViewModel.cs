using KKTraders.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models.ViewModel
{
    public class OrderViewModel
    {
        public OrderHeader orderHeader { get; set; }
        public IEnumerable<OrderDetails> orderDetails { get; set; }

    }
}
