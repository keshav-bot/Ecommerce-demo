using KKTraders.Data;
using KKTraders.Extensions;
using KKTraders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NotificationAndAlert : Controller
    {
        private readonly ApplicationDbContext _context;
        public NotificationAndAlert(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ProductModel> products = new List<ProductModel>();        
            var product = _context.Product.ToList();///this code gets data from database
            foreach (var item in product)//get each item from the  list one by one 
            {
                if (item.Units < item.MinimumUnits)//checks 
                {
                    products.Add(new ProductModel()//send to html page 
                    {
                        Name = item.Name,
                        Price = item.Price,
                        Id = item.Id,
                        Units = item.Units,
                        MinimumUnits = item.MinimumUnits,
                       DateOfPurchase = item.DateOfPurchase
                    }) ;
                }              
            }
            return View(products);
        }
    }
}
