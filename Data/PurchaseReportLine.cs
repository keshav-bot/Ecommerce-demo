using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace KKTraders.Data
{
    public class PurchaseReportLine
    {
        public int Id { get; set; }
        public PurchaseReport purchaseReportId { get; set; }
        public PurchaseReport purchaseReport { get; set; }

        [Required]
        [StringLength(50)]
        public string PurchaseId { get; set; }

        public Product product { get; set; }
        [Display(Name = "Qty")]
        public double quantity { get; set; }
      
        [Display(Name = "Item Price")]
        public decimal price { get; set; }

        [Display(Name = "Discount Amount")]
        public decimal discountAmount { get; set; }

        [Display(Name = "Total Amount")]
        public decimal totalAmount { get; set; }
    }
}

