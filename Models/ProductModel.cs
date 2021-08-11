using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Name of Product")]
        public String Name { get; set; }
        [Required]
        [Display(Name ="Price of Product")]
        public double Price { get; set; }
        [Required]
        [Display(Name ="Unit")]
        public double Units { get; set; }
        public double MinimumUnits { get; set; }
        [Required]
        public DateTime DateOfPurchase { get; set; }
        
        public String Description { get; set; }
        public string ProductImageUrl { get; set; }
        [BindProperty(SupportsGet =true)]
        public string SearchProduct { get; set; }
        public IFormFile ProductImage { get; set; }
        [Required]
        public int  CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public int frequency { get; set; }
        [DataType(DataType.Currency)]
        public int Discount { get; set; }
        public int SupplierId { get; set; }
    }
}
