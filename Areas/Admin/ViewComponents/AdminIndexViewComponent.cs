using KKTraders.Areas.Admin.Models;
using KKTraders.Data;
using KKTraders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Areas.ViewComponents
{
    public class AdminIndexViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public AdminIndexViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var product =await  _context.Product.ToListAsync();
            int count = 0;
            foreach (var item in product)
            {
                if (item.Units < item.MinimumUnits)
                {
                    count++;
                }
            }
            return View(count);
        }

    }
}
