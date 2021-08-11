using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Data
{
    public class ProductInvoice
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public double Quantity { get; set; }
        public double  Price { get; set; }
        
        public int SalesReportId { get; set; }
        [ForeignKey("SalesReportId")]
        public SalesReport invoice { get; set; }
    }
}
