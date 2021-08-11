using KKTraders.Areas.Admin.Models;
using KKTraders.Data;
using KKTraders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AdminController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;        
        }
      
        public IActionResult Index()
        {
            var NoOfProduct = _context.Product.Count();
            var NoOfEmployee = _context.Employees.Count();
            var NoOfUsers = _context.Users.Count();
            var NoOfPendingOrders = _context.orderHeader.Where(x => x.Status == "Pending").Count();
            var employee = _context.Employees.ToList();
            var product = _context.Product.ToList();
            int count = 0;
            var noOfSupplier = _context.Supplier.Count();
            AdminIndexModel model = new AdminIndexModel()
            {
                TotalUser = NoOfUsers,
                Employee = NoOfEmployee,
                NoOfProduct = NoOfProduct,
                NoOfPendingOrder = NoOfPendingOrders,
                employee = new List<Employee>(),
                product = new List<Data.Product>(),
                NotificationCount = count,
                numberOfSupplier=noOfSupplier,
            };
            foreach (var item in employee)
            {
                model.employee.Add(new Employee()
                {
                    Id = item.Id,
                    Name = item.Name,
                    EmployeeImageUrl = item.EmployeeImageUrl,
                    PhoneNumber = item.PhoneNumber,
                });
            }
            foreach (var item in product)
            {
                if (item.MinimumUnits < item.MinimumUnits)
                {
                    count++;
                }

                model.product.Add(new Data.Product()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    ProductImageUrl = item.ProductImageUrl,
                    DateOfPurchase = item.DateOfPurchase
                });
            }
            return View(model);
        }
    
      
        [Authorize(Roles = "Admin")]
        public  async Task<IActionResult> Logout()
        {
            await  _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AddQuantity()
        {
            ViewBag.productName = new SelectList(await _context.Product.ToListAsync(), "Id", "Name");
            AddQuantity quantity = new AddQuantity();
            quantity.Updated = false;
            return View(quantity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuantity(AddQuantity model)
        {
            if(ModelState.IsValid)
            {
                var product = await _context.Product.Where(x => x.Id == model.ProductId).FirstOrDefaultAsync();
                ViewBag.productName = new SelectList(await _context.Product.ToListAsync(), "Id", "Name");
                if (product!=null && model.Quantity>0)
                {
                    product.Units = product.Units+model.Quantity;
                    _context.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Updated = true;
                    return View();
                }
                ModelState.AddModelError("","Quantity should be more than 0");
                return View(model);
            }
            ViewBag.Updated = false;
            ViewBag.productName = new SelectList(await _context.Product.ToListAsync(), "Id", "Name");
            return View(model);

        }
        public IActionResult AllProduct()
        {
            return Json(new { data = _context.Product.ToList()});
        }

    }
}
