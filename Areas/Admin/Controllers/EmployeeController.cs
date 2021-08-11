using KKTraders.Data;
using KKTraders.Models;
using KKTraders.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepo;
        private readonly PostRepository _postRepo;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public EmployeeController(EmployeeRepository employeeRepo, PostRepository postRepo,ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _employeeRepo = employeeRepo;
            _postRepo = postRepo;
            _webHostEnvironment=webHostEnvironment;
            
        }
        public  async Task<IActionResult> Index()
        {
            var employee= await _employeeRepo.GetAllEmployee();
            return View(employee);
        }
        [HttpGet]
       
        public  async Task<IActionResult> AddNewEmployee(bool isSubmitted = false)
        {
            ViewBag.isSubmitted = isSubmitted;
            ViewBag.posts = new SelectList( await _postRepo.GetAllPost(),"Id","Name");
            return View();
        }
        [HttpPost]
       
        public async Task<IActionResult> AddNewEmployee(EmployeeModel model , string returnUrl)
        {
   
                if (ModelState.IsValid)
                {
                     string uniqueFileName = null;
                if (model.EmployeeImage != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath.ToString(), "/Images/EmployeeImage/");
                        string root = _webHostEnvironment.WebRootPath.ToString();
                        string test = CombinePath(root, uploadsFolder);
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.EmployeeImage.FileName;
                        model.EmployeeImageUrl = CombinePath("/Images/EmployeeImage/",uniqueFileName);
                        string filePath = CombinePath(test, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            model.EmployeeImage.CopyTo(fileStream);
                        }


                    }
                    await _employeeRepo.AddEmployee(model);
                    return RedirectToAction(nameof(AddNewEmployee), new { isSubmitted = true });
                }
                ViewBag.isSubmitted = false;
                ViewBag.posts = new SelectList(await _postRepo.GetAllPost(), "Id", "Name");
                return View();    
           
        }
        public  async  Task<IActionResult> GetEmployeeById(int id)
        {
            var employee =  await _employeeRepo.GetEmployeeById(id);
            return View(employee);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int id)
        { 
            string filepath = await _employeeRepo.DeleteEmployee(id);
            if(filepath != null)
            {
                String hostEnv = _webHostEnvironment.WebRootPath.ToString();
                String fullpath = hostEnv + filepath;
                System.IO.File.Delete(fullpath);
            }
            ViewBag.isDeleted = false;
            return RedirectToAction(nameof(Index),new { isDeleted=true});
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditEmployee( int id)
        {
            var emp =   await _employeeRepo.EditEmployeeById(id);
            ViewBag.posts = new SelectList(await _postRepo.GetAllPost(), "Id", "Name");
            return View(emp);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditEmployee( int id , EditEmployeeViewModel model)
        {
             await _employeeRepo.EditEmployee(id,model);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public  async Task<IActionResult> EmployeeDetails(int id)
        {
            var a = await _employeeRepo.GetEmployeeById(id);
            return View(a);
        }

        private async Task<string> setImage(string path, IFormFile file)
        {

            string rootimageurl =   _webHostEnvironment.WebRootPath.ToString();
            string actualPath = rootimageurl + path + Guid.NewGuid() + "_" + file.FileName;
            return actualPath;

        }

        private string CombinePath(string s1, string s2)
        {
            if(s1!=null && s2!=null)
            {
                return s1.Trim().TrimEnd(System.IO.Path.DirectorySeparatorChar)
                
                 + s2.Trim().TrimStart(System.IO.Path.DirectorySeparatorChar);
            }
            return null;
        }

    }
}
