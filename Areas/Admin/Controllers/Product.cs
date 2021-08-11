using KKTraders.Data;
using KKTraders.Models;
using KKTraders.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Controllers
{
    [Authorize(Roles="Admin")]
    [Area("Admin")]
    public class Product : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CategoryRepository _categoryRepository;
        private readonly ApplicationDbContext _context;
        public Product(ProductRepository productRepository,IWebHostEnvironment webHostEnvironmet, CategoryRepository categoryRepository,
                ApplicationDbContext context)
        {
            _context = context;
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironmet;
            _categoryRepository = categoryRepository;
        }
        public  async Task<IActionResult> AllProduct()
        {
            var productDetails =  await _productRepository.GetAllProduct();
            return View(productDetails);
        }
        public async Task<IActionResult> AddNewProduct()
        {

            ViewBag.Category =  new SelectList( await _categoryRepository.GetAllCategory(),"Id","Title");
            ViewBag.Supplier = new SelectList(await _context.Supplier.ToListAsync(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductModel model)
        {
            string uniqueFileName = null;
            if (ModelState.IsValid)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath.ToString(), "/Images/ProductImage/");
                string root = _webHostEnvironment.WebRootPath.ToString();
                string test = CombinePath(root, uploadsFolder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
                model.ProductImageUrl = CombinePath("/Images/ProductImage/", uniqueFileName);
                string filePath = CombinePath(test, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProductImage.CopyTo(fileStream);
                }
                int id1 = await _productRepository.AddNewProduct(model);
                return RedirectToAction(nameof(AddNewProduct), new { isSubmitted = true });

            }
            return View();
        }
        public async Task<IActionResult> DeleteProduct(int id)
        {
             string imageUrl=await _productRepository.DeleteProduct(id);
            if(imageUrl !=null)
            {
                var rootfilename = _webHostEnvironment.WebRootPath.ToString();
                var ImageFolder = rootfilename + imageUrl;

               System.IO.File.Delete(ImageFolder);
            }
            return RedirectToAction(nameof(AllProduct));
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await  _productRepository.GetProductById(id);
            return View(product);
        }
        public async Task<IActionResult> EditProduct(int id)
        {
           var product= await _productRepository.GetProductById(id);
            ViewBag.category = new SelectList(await _categoryRepository.GetAllCategory() ,"Id", "Title");
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, ProductModel model)
        {
            if(ModelState.IsValid)
            {
               await _productRepository.EditProduct(id, model);
                ViewBag.category = new SelectList(await _categoryRepository.GetAllCategory(), "Id", "Title");
            }
            ViewBag.category = new SelectList(await _categoryRepository.GetAllCategory(), "Id", "Title");
            return View(model);
        }



        [HttpPost]
        public  async Task<IActionResult> searchProduct(string searchBy )
        {
            var product = await _productRepository.searchProduct(searchBy);

            return Json(JsonConvert.SerializeObject(product));
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
