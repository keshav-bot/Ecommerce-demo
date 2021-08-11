using KKTraders.Data;
using KKTraders.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Controllers
{
    [AllowAnonymous]
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Search(String SearchString)
        {
            var product = _context.Product.Where(x => x.Name == SearchString).Include("Category").FirstOrDefault();
            if (product == null)
            {
                ViewBag.ErrorMessage = $"Product with name {SearchString} could not be found";
                return View("NotFound");
            }
            else
            {
                var simillarProduct = _context.Product.Where(x => x.Category == product.Category).ToList();
                SearchModel model = new SearchModel()
                {
                    Searchvm = new SearchViewModel(),
                    simillarSearch = new List<SimillarSearchModel>()
                };
                model.Searchvm = new SearchViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    ProductImageUrl = product.ProductImageUrl,
                    Discount = product.Discount,

                };
                foreach (var allproduct in simillarProduct)
                {
                    if(allproduct!=product)
                    {
                        model.simillarSearch.Add(new SimillarSearchModel()
                        {
                            Id = allproduct.Id,
                            Name = allproduct.Name,
                            Price = allproduct.Price,
                            Description = allproduct.Description,
                            units = allproduct.Units,
                            ProductImageUrl = allproduct.ProductImageUrl,
                        });
                    }

                }
                return View(model);
            }
        }


    }
}
