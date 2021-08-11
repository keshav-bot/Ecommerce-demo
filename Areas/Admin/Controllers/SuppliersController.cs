using KKTraders.Data;
using KKTraders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SuppliersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SuppliersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var supplier = await _context.Supplier.ToListAsync();
            SupplierModel model = new SupplierModel()
            {
                supplier = new List<Data.Supplier>(),
            };
            if (supplier != null)
            {
                foreach (var item in supplier)
                {
                    model.supplier.Add(new Data.Supplier()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PhoneNumber=item.PhoneNumber,
                        Address=item.Address,
                        status=item.status
                    });

                }
                return View(model);
            }
            return View(model);
        }
        //httppost for supplier
        [HttpPost]
        public async Task<IActionResult> Index(SupplierModel model)
        {
            var supplier = await _context.Supplier.ToListAsync();
            SupplierModel model1 = new SupplierModel()
            {
                supplier = new List<Data.Supplier>(),
            };
       
            foreach (var item in supplier)
            {
                model1.supplier.Add(new Data.Supplier()
                {
                    Id = item.Id,
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    Address = item.Address,
                    status = item.status
                });
                
            }
            if (ModelState.IsValid)
            {
               
                if (supplier.Count > 0)
                {
                    foreach (var item in supplier)
                    {
                        if(item.Name.Contains(model.Name))
                        {
                            ModelState.AddModelError("", "Supplier is already added");
                            return View(model1);
                        }
                    }
                }
                Supplier dbsupplier = new Supplier()
                {
                    Id = model.Id,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    status = 1
                };
                _context.Supplier.Add(dbsupplier);
                ViewBag.success = true;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(model1);

        }



        // GET: SuppliersController/Details/5
        public ActionResult Details(int id)
        {
            var supplier = _context.Supplier.Where(x=>x.Id==id).FirstOrDefault();
            if(supplier !=null)
            {
                return View(supplier);
            }
            return RedirectToAction(nameof(Index));
          
        }

        // GET: SuppliersController/Create
        public async Task<ActionResult> Create()
        {
            var supplier = await _context.Supplier.ToListAsync();
            SupplierModel model = new SupplierModel()
            {
                supplier = new List<Data.Supplier>(),
            };
            if (supplier != null)
            {
                foreach (var item in supplier)
                {
                    model.supplier.Add(new Data.Supplier()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PhoneNumber = item.PhoneNumber,
                        Address = item.Address,
                        status = item.status
                    });

                }
                return View(model);
            }
            return View(model);
        }

        // POST: SuppliersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SupplierModel model)
        {
            var supplier = await _context.Supplier.ToListAsync();
            SupplierModel model1 = new SupplierModel()
            {
                supplier = new List<Data.Supplier>(),
            };

            foreach (var item in supplier)
            {
                model1.supplier.Add(new Data.Supplier()
                {
                    Id = item.Id,
                    Name = item.Name,
                    PhoneNumber = item.PhoneNumber,
                    Address = item.Address,
                    status = item.status
                });

            }
            if (ModelState.IsValid)
            {

                if (supplier.Count > 0)
                {
                    foreach (var item in supplier)
                    {
                        if (item.Name.Contains(model.Name))
                        {
                            ModelState.AddModelError("", "Supplier is already added");
                            return View(model1);
                        }
                    }
                }
                Supplier dbsupplier = new Supplier()
                {
                    Id = model.Id,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    status = 1
                };
                _context.Supplier.Add(dbsupplier);
                ViewBag.success = true;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(model1);
        }

        // GET: SuppliersController/Edit/5
        public ActionResult Edit(int id)
        {
          var supplier=  _context.Supplier.Where(x => x.Id == id).FirstOrDefault();
            if(supplier!=null)
            {
                return View(supplier);
            }
            return NotFound();      
        }
        // POST: SuppliersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Supplier.Update(supplier);
                    _context.SaveChanges();
                   
                }
                catch(Exception)
                {

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
          
        }
        // POST: SuppliersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
                var supplier = _context.Supplier.Where(x => x.Id == id).FirstOrDefault();
                _context.Supplier.Remove(supplier);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
        }
    }
}
