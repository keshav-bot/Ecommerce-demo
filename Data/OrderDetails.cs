using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Data
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public OrderHeader OrderHeader { get; set;}
        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int discount { get;set; }
    }
}
