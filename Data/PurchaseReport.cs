using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Data
{
    public class PurchaseReport
    {

        public PurchaseReport()
        {
            DateOfPurchase = DateTime.UtcNow;
        }
        [Key]
        public int purchaseReportId { get; set; }
  
        [Required]
        public DateTime DateOfPurchase { get; set; }
        [Display(Name = "Total Discount")]
        public decimal totalDiscountAmount { get; set; }
        [Display(Name = "Total Order")]
        public decimal totalpurchaseAmount { get; set; }
        public int SupplierId { get; set; }
        public Supplier supplier { get; set; }   
        public List<PurchaseReportLine> purchaseReportLine { get; set; }
      
    }
}
