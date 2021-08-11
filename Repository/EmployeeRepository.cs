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
    public class EmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
      
        public EmployeeRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
           
        }
        public async Task AddEmployee(EmployeeModel employee)
        {
            var addemployee = new Employee()
            {
                Name = employee.Name,
                Address = employee.Address,
                PhoneNumber = employee.PhoneNumber,
                Nationality = employee.Nationality,
                PostId = employee.PostId,
                EmployeeImageUrl=employee.EmployeeImageUrl
          
            };
            await  _context.Employees.AddAsync(addemployee);
             await _context.SaveChangesAsync();
            return ;
        }
        public async Task<List<EmployeeModel>> GetAllEmployee()
        { 

            var allEmployee = await _context.Employees.Select(x => new EmployeeModel()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                PhoneNumber = x.PhoneNumber,
                Nationality = x.Nationality,
                PostName = x.Post.Name,
                PostId = x.PostId,
                EmployeeImageUrl=x.EmployeeImageUrl
            }).ToListAsync();
            return allEmployee;
        }
        public async Task<EmployeeModel> GetEmployeeById( int id)
        {

            return  await _context.Employees.Where(x => x.Id == id).Select(x =>
                new EmployeeModel()
                {
                    Id=x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    PhoneNumber = x.PhoneNumber,
                    Nationality = x.Nationality,
                    PostId=x.PostId,
                    PostName=x.Post.Name,
                    Salary=x.Post.salary,
                    Description=x.Post.JobDescription,
                    EmployeeImageUrl=x.EmployeeImageUrl
                    
                    
                }).FirstOrDefaultAsync();
        }
        public async Task<EditEmployeeViewModel> EditEmployeeById(int id)
        {

            return await _context.Employees.Where(x => x.Id == id).Select(x =>
               new EditEmployeeViewModel()
               {
                   Id = x.Id,
                   Name = x.Name,
                   Address = x.Address,
                   PhoneNumber = x.PhoneNumber,
                   Nationality = x.Nationality,
                   PostId = x.PostId,
                   EmployeeImageUrl = x.EmployeeImageUrl
               }).FirstOrDefaultAsync();
        }
        public async Task<String> DeleteEmployee(int id)
        {
         
              var employee= await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync() ;
                ////var filepath= Path.Combine(_webHostEnvironment.WebRootPath, employee.EmployeeImageUrl);
                //string webhost = _webHostEnvironment.WebRootPath;
                //string filepath = webhost + employee.EmployeeImageUrl;
                //File.Delete(filepath);
                _context.Employees.Remove(employee);
                await  _context.SaveChangesAsync();
                return employee.EmployeeImageUrl; 
        }
        public async Task<string> EditEmployee(int id , EditEmployeeViewModel model)
        {

            var emp =  await _context.Employees.Where(x => x.Id == id).FirstOrDefaultAsync();
            emp.Name = model.Name;
            emp.Nationality = model.Nationality;
            emp.Address = model.Address;
            emp.PhoneNumber = model.PhoneNumber;
            emp.PostId = model.PostId;
            if(model.EmployeeImage != null)
            {
                var fileName = "/Images/EmployeeImage/";
                var currentfile = _webHostEnvironment.WebRootPath.ToString() + model.EmployeeImageUrl;
                File.Delete(currentfile);
                emp.EmployeeImageUrl = ProcessImage(fileName, model.EmployeeImage);
            }
            else
            {
                emp.EmployeeImageUrl = model.EmployeeImageUrl;
            }
             _context.Employees.Update(emp);
            _context.SaveChanges();
            return "Sucessfully updated";
        }
       private  string ProcessImage(string fileName,IFormFile file)
        {
            string uploadsFolder = CombinePath( _webHostEnvironment.WebRootPath, fileName);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
              
            string filePath = CombinePath(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(fileStream);
            }
           var imgpath= CombinePath(fileName, uniqueFileName);
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
