using KKTraders.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models.ViewModel
{
    public class ShoppingCartviewModel
    {
        public string ItemId { get; set; }

        public string CartId { get; set; }

        public double Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }

        public int ProductId { get; set; }

        public virtual Product product { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string productImage { get; set; }
    }
}
