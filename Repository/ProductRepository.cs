using KKTraders.Data;
using KKTraders.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Repository
{
    
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductRepository(ApplicationDbContext context, IWebHostEnvironment webHostenvironment)
        {
            _context = context;
            _webHostEnvironment = webHostenvironment;
        }
        public async Task<List<ProductModel>> GetAllProduct()
        {
                var product = await _context.Product.Select(x => new ProductModel()
                {
                    Name = x.Name,
                    Price = x.Price,
                    Id = x.Id,
                    Units=x.Units,
                    DateOfPurchase = x.DateOfPurchase,
                    Description = x.Description,
                    ProductImageUrl = x.ProductImageUrl,
                    MinimumUnits=x.MinimumUnits,
                    
                }).ToListAsync();
                return product;

            
           
          
        }
        public async Task<int> AddNewProduct(ProductModel model)
        {
            Product product = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Id = model.Id,
                Units=model.Units,
                DateOfPurchase = DateTime.Now,
                Description = model.Description,
                ProductImageUrl=model.ProductImageUrl,
                CategoryId=model.CategoryId  ,
                Discount=model.Discount,
                MinimumUnits=model.MinimumUnits,
                SupplierId=model.SupplierId
            };
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return 1;

        }
        public async Task<string> DeleteProduct(int id)
        {
           var product = await _context.Product.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Product.Remove(product);
            _context.SaveChanges();
            return product.ProductImageUrl;
            
        }
        public async Task<ProductModel> GetProductById( int id)
        {
             var product=  await  _context.Product.Where(x => x.Id == id).Select(x =>new ProductModel() {

                Name = x.Name,
                Price = x.Price,
                Id = x.Id,
                Units=x.Units,
                DateOfPurchase = DateTime.Now,
                Description = x.Description,
                ProductImageUrl = x.ProductImageUrl,
                CategoryId=x.CategoryId,
                Discount=x.Discount,
                CategoryTitle=x.CategoryTitle,
                SupplierId=x.SupplierId,
                MinimumUnits=x.MinimumUnits
                

            }).FirstOrDefaultAsync();
            return product;
           

        }
        public async Task<List<ProductModel>> searchProduct(string search)
        {

            var product =  await _context.Product.Where(x => x.Name.StartsWith(search) || search==null).Select(y => new ProductModel()
            {
                Name = y.Name,
                Id=y.Id,
                Price = y.Price,
                Description = y.Description,
                ProductImageUrl = y.ProductImageUrl,
                Units=y.Units,
                MinimumUnits=y.MinimumUnits,
            }).ToListAsync();

            return product ;
        }

       public async Task EditProduct(int id ,ProductModel model)
        {
            var product = _context.Product.Where(x => x.Id == id).FirstOrDefault();
            product.Name = model.Name;
            product.Price = model.Price;
            product.MinimumUnits = model.MinimumUnits;
            product.Units = model.Units;
            product.Discount = model.Discount;
            if(model.ProductImage != null)
            {
                var fileName = "/Images/ProductImage/";
                var currentfile = _webHostEnvironment.WebRootPath + model.ProductImageUrl;
                File.Delete(currentfile);
                product.ProductImageUrl =   ProcessImage(fileName, model.ProductImage);     
            }
            else
            {
                product.ProductImageUrl = model.ProductImageUrl;
            }
            
             product.CategoryId = model.CategoryId;
            _context.Product.Update(product);
            _context.SaveChanges();
            
        }
        private string ProcessImage(string fileName, IFormFile file)
        {
            string uploadsFolder = CombinePath(_webHostEnvironment.WebRootPath, fileName);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            string filePath = CombinePath(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(fileStream);
            }
            var imgpath = CombinePath(fileName, uniqueFileName);
            return imgpath;
        }
        private string CombinePath(string s1, string s2)
        {
            if (s1 != null && s2 != null)
            {
                return s1.Trim().TrimEnd(System.IO.Path.DirectorySeparatorChar)

                 + s2.Trim().TrimStart(System.IO.Path.DirectorySeparatorChar);
            }
            return null;
        }

    }
}
