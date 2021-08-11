using KKTraders.Data;
using KKTraders.Models;
using KKTraders.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly ApplicationDbContext _context;
        public CategoryController(CategoryRepository categoryRepository, ApplicationDbContext context)
        {
            _categoryRepository = categoryRepository;
            _context = context;
        }
        public  async Task<IActionResult> Index()
        {
            var category = await _categoryRepository.GetAllCategory();
            CategoryModel model = new CategoryModel()
            {
                category = new List<Data.Category>(),
            };
            if (category != null)
            {
               
                foreach (var item in category)
                {
                    model.category.Add(new Data.Category()
                    {
                        Id=item.Id,
                        Title=item.Title
                    });
                  
                }
                return View(model);
            }
            return View(model);


        }
        //[HttpGet]
        //public IActionResult AddNewCategory()
        //{

        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> Index(CategoryModel model)
        {
            var category = await _categoryRepository.GetAllCategory();
            CategoryModel model1 = new CategoryModel()
            {
                category = new List<Data.Category>(),
            };
            foreach (var item in category)
            {
                model1.category.Add(new Data.Category()
                {
                    Id = item.Id,
                    Title = item.Title
                });
            }
            if (ModelState.IsValid)
            {  
                if(category.Count>0)
                {
                    foreach (var item in category)
                    {
                        if (item.Title.Contains(model.Title))
                        {
                            ModelState.AddModelError("", "Category is already added");
                            return View(model1);
                        }
                    }
                }
                await _categoryRepository.AddCategory(model);
                ViewBag.success = true;
                return RedirectToAction(nameof(Index));

            }       
            return View(model1); 
           
        }

        public async Task<IActionResult> Edit(int id )
        {
            var category =await _categoryRepository.GetCategoryById(id);
            if(category != null)
            {
                return View(category);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryModel model)
        {
            if(ModelState.IsValid)
            {
                Category category = new Category()
                {
                    Id = model.Id,
                    Title = model.Title
                };
                _context.Category.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


            public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryRepository.DeleteCategory(id);
            return RedirectToAction("Index", "Category");
        }

    }
}
