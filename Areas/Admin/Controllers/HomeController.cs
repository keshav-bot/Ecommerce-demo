using KKTraders.Areas.Admin.Models;
using KKTraders.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        [Route("ns-admin")]
        public IActionResult Index()
        {
            var NoOfProduct = _context.Product.Count();
            var NoOfEmployee = _context.Employees.Count();
            var NoOfUsers = _context.Users.Count();
            var NoOfPendingOrders = _context.orderHeader.Where(x => x.Status == "Pending").Count();
            var employee = _context.Employees.ToList();
            var product = _context.Product.ToList();
            int count = 0;
            var noofSupplier = _context.Supplier.Count();
            AdminIndexModel model = new AdminIndexModel()
            {
                TotalUser = NoOfUsers,
                Employee = NoOfEmployee,
                NoOfProduct = NoOfProduct,
                NoOfPendingOrder = NoOfPendingOrders,
                employee = new List<Employee>(),
                product = new List<Data.Product>(),
                NotificationCount = count,
                numberOfSupplier=noofSupplier
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
    }
}
