using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models.ViewModel
{
    public class ProductCategory
    {
        public String Name { get; set; }
        public double Price { get; set; }
        public double Units { get; set; }
        public double MinimumUnits { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public String Description { get; set; }
        public string ProductImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public string Title { get; set; }
    }
}
