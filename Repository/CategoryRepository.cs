using KKTraders.Data;
using KKTraders.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Repository
{
    public class CategoryRepository
    {

        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context; 
        }
        public async Task<String> AddCategory(CategoryModel model)
        {
            var category = new Category()
            {
                Id = model.Id,
                Title = model.Title
            };
            _context.Category.Add(category);
           await _context.SaveChangesAsync();
            return "";
        }
        public async Task<List<CategoryModel>> GetAllCategory()
        {
            var category = await  _context.Category.Select(x => new CategoryModel()
            {
                Id = x.Id,
                Title = x.Title
            }).ToListAsync();
            return category;
        }

        public async Task<CategoryModel> GetCategoryById(int id)
        {
            Category category =await _context.Category.Where(x => x.Id == id).FirstOrDefaultAsync();
            CategoryModel model = new CategoryModel()
            {
                Id = category.Id,
                Title=category.Title

            };
            return model;   
        }
        public async Task<string> DeleteCategory(int id)
        {
            var category =  await _context.Category.Where(x => x.Id == id).FirstOrDefaultAsync();
             _context.Category.Remove(category);
            _context.SaveChanges();
            return "";
        }
        
    }
}
