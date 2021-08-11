using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Data
{
    public class SalesReport
    {
        public int Id { get; set; }
       
        [StringLength(40)]
        public string SalesId { get; set; }

        
        public DateTime DateOfSale { get; set; }        
      
        [StringLength(30)]
        
        public string NameOfCustomer { get; set; }
        [EmailAddress]
       
        public  string Email { get; set; }
       
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        public IList<Product> product { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
       
        [StringLength(40)]
        public string Address1 { get; set; }
        [StringLength(40)]
        public string Address2 { get; set; }
        public double Quantity { get; set; }


        
    }
}
