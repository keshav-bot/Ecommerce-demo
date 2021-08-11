using KKTraders.Data;
using KKTraders.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class PurchaseReport : Controller
    {
        private readonly ApplicationDbContext _context;
        public PurchaseReport(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult SalesInvoice()
        {
            return View();
        }

        public IActionResult Details(int id)
        {

            return View();
        }

        #region API Calls
        [HttpGet]
        public IActionResult AllPurchase()
        {
            return Json(new { data =  _context.SalesReport.ToList() });
        }
        #endregion
    }
}
