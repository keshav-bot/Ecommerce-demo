using KKTraders.Data;
using KKTraders.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesReport : Controller
    {
        private readonly ApplicationDbContext _context;
        public SalesReport(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            
            List<salesReportDetails> salesReportDetails = new List<salesReportDetails>();
            var details = _context.productInvoice.Where(x => x.SalesReportId == id).ToList();
            if(details!=null)
            {
                foreach (var item in details)
                {
                    salesReportDetails.Add(new KKTraders.Models.ViewModel.salesReportDetails()
                    {
                        productName = item.Name,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        sum = item.Price * item.Quantity
                    });
                }
                return View(salesReportDetails);
            }
            ViewBag.ErrorMessage = "Record cannot be found";
            return View("NotFound");
        }

        #region API Calls
        [HttpGet]
        public IActionResult AllSales()
        {
            return Json(new { data = _context.SalesReport.ToList() });
        }
        #endregion
    }
}
