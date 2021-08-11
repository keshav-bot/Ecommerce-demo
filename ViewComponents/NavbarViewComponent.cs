using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KKTraders.Data;
using KKTraders.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KKTraders.Components
{
    public class NavbarViewComponent: ViewComponent
    {
        private readonly ApplicationDbContext _context;
        public NavbarViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category = await _context.Category.ToListAsync();
            var goods = await _context.Product.ToListAsync();
            ProductCategoryViewModel model = new ProductCategoryViewModel() {
                category=new List<Category>(),
                product=new List<Product>()
            };

            foreach(var item in category)
            {
                model.category.Add(item);
            }
            foreach (var item in goods)

            {
                model.product.Add(item);
            }

            return View(model);

        }

    }
}
