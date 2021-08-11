using KKTraders.Data;
using KKTraders.Extensions;
using KKTraders.Models;
using KKTraders.Models.ViewModel;
using KKTraders.Repository;
using KKTraders.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StaticDetails;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
namespace KKTraders.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductRepository _productRepo;
        private readonly ApplicationDbContext _context;
        private readonly IEmailService  _emailService;
        public HomeController(ILogger<HomeController> logger, ProductRepository productRepo, ApplicationDbContext context,IEmailService emailService)
        {
            _logger = logger;
            _productRepo = productRepo;
            _context = context;
            _emailService = emailService;
        }
        [AllowAnonymous]    
        
        public IActionResult Index()
        {

            var product = _context.Product.ToList();
            return View(product);
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //public IActionResult AddToCart(int id)
        //{
        //    List<int> productList = new List<int>();
        //    if (string.IsNullOrEmpty(HttpContext.Session.GetString(SD.sessionCart)))
        //    {
        //        productList.Add(id);
        //        HttpContext.Session.SetObject(SD.sessionCart, productList);

        //    }
        //    else
        //    {
        //        productList = HttpContext.Session.GetObject<List<int>>(SD.sessionCart);
        //        if (!productList.Contains(id))
        //        {
        //            productList.Add(id);
        //            HttpContext.Session.SetObject(SD.sessionCart, productList);
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
        [Route("Remove-Product")]
        public IActionResult Remove(int id)
        {
            List<int> sessionList = new List<int>();
            sessionList = HttpContext.Session.GetObject<List<int>>(SD.sessionCart);
            if (sessionList.Contains(id))
            {
                sessionList.Remove(id);

            }
            HttpContext.Session.SetObject(SD.sessionCart, sessionList);
            return RedirectToAction("Index", "Cart");
        }
        [Route("AllProduct")]
        public IActionResult AllProduct()
        {
            var allProduct = _context.Product.ToList();
            return View(allProduct);
        }
        [Route("Product-details")]
        public IActionResult ProductDetails(int id)
        {
            var product = _context.Product.Where(x=>x.Id==id).FirstOrDefault();
            return View(product);
        }

        [Route("Search-Product")]
        public IActionResult SearchCategory(String productCategory)
        {
            if(productCategory !=null)
            {
                var product = _context.Product.Where(x => x.Category.Title== productCategory).ToList();
                return View(product);               
            }
            else
            {
                return View("NotFound");
            }

            //if(productCategory==null)
            //{
               
            //    ViewBag.ErrorMessage = "Search Box is empty";
            //    return View("ProductNotfound");
            //}
            //else
            //{
            //    if(productCategory=="ElectronicEquipment")
            //    {
            //        var product = _context.Product.Where(x => x.Category.Title == "ElectronicEquipment").ToList();
            //        return View(product);

            //    }
            //    else if(productCategory=="Solar")
            //    {
            //        var product = _context.Product.Where(x => x.Category.Title == "Solar").ToList();
            //        return View(product);
            //    }
            //    else if (productCategory == "Machinery")
            //    {
            //        var product = _context.Product.Where(x => x.Category.Title == "Machinery").ToList();
            //        return View(product);
            //    }
            //    else if (productCategory == "AgriculturalEquipment")
            //    {
            //        var product = _context.Product.Where(x => x.Category.Title == "AgriculturalEquipment").ToList();
            //        return View(product);
            //    }
            //    else
            //    {
            //        ViewBag.MessageHeader = "Not Found Error!";
            //        ViewBag.ErrorMessage = "Unable to find the search product";
            //        return View("ProductNotfound");
            //    }
            //}
        }
    }
}

