using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Data
{
    public class Product
    {
        public int Id { get; set; }
       
        public String Name { get; set; }
        
        public double Price { get; set; }
        
        public double Units { get; set; }
        public double MinimumUnits { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public String Description { get; set; }
        public string ProductImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int Discount { get; set; }

        public string CategoryTitle { get; set; }

        public Category Category { get; set; }
        
        public Supplier Supplier { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
    }
}
