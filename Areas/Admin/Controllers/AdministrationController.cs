using KKTraders.Models;
using KKTraders.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
           _userManager = userManager;
        }
        [Route("User-List")]
        public IActionResult ListUser()
        {
            var users =  _userManager.Users.ToList();

            return View(users);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = model.Name
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("DisplayRole","Administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult DisplayRole()
        {
           var  roles = _roleManager.Roles;
            return View(roles);
           
        }
        public async Task<IActionResult> EditRole(string id)
       {
            var role = await  _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"The Role was{id} not found";
                return View("NotFound");
            }

           var model = new EditRoleViewModel
           {
              Id=role.Id,
              RoleName=role.Name,

          };
          foreach (var user in _userManager.Users)
            {
                if(await _userManager.IsInRoleAsync(user,role.Name))
                {
                            model.User.Add(user.UserName);
                }
            }

            return View(model);

       }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"The Role was{model.RoleName} not found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                 var result =await _roleManager.UpdateAsync(role);
                if(result.Succeeded)
                {
                    return RedirectToAction("DisplayRole");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }   
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = _roleManager.Roles.Where(x => x.Id == id).FirstOrDefault();
            if(role==null)
            {
                ViewBag.ErrorMessage = $"Role with id :{id} could not be found";
                return View("NotFound");
            }
            else {
                try
                {
                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("DisplayRole", "Administration");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return RedirectToAction("DisplayRole", "Administration");

                }
                catch(DbUpdateException ex)
                {
                    ViewBag.ErrorTitle = $"{role.Name} is in use";
                    ViewBag.ErrorMessage = $"'{role.Name}'  role that you are trying to delete  is being used by other resource." +
                        "Delete all those releated resources and try again. REFERENCE: The Flollowing role contain Users . You should delete " +
                        "all related User to be able to delete Role. ";
                    return View("Error");
                }
                   
            }

        }
        [HttpGet]
        public async Task<IActionResult> EditUserInRole(String roleId)
        {
             var userRole= await _roleManager.FindByIdAsync(roleId);
            ViewBag.RoleId = roleId;
            if(userRole ==null)
            {
                ViewBag.ErrorMessage = $"Role {roleId} Could not be found";
                return View("NotFound");
            }
            var model = new List<EditUserInRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                var editUserInRoleViewModel = new EditUserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if(await _userManager.IsInRoleAsync(user,userRole.Name))
                {
                    editUserInRoleViewModel.IsSelected = true;
                }
                else
                {
                    editUserInRoleViewModel.IsSelected = false;
                }
                model.Add(editUserInRoleViewModel);

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<EditUserInRoleViewModel> model , string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role {roleId} Could not be found";
                return View("NotFound");
            }

            for(int i=0; i<model.Count;i++)
            {
              var user=  await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if(model[i].IsSelected && !(await _userManager.IsInRoleAsync(user,role.Name)))
                {
                   result= await _userManager.AddToRoleAsync(user, role.Name);

                }
                else if(!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
               if(result.Succeeded)
                {
                    if(i<(model.Count-1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("EditRole",new { Id=roleId});
                    }
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user =_userManager.Users.Where(x=>x.Id==id).FirstOrDefault();
            if(user==null)
            {
                ViewBag.ErrorMessage = $"User with id {id } could not be found";
                return View("NotFound");
            }
                var userClaims =await  _userManager.GetClaimsAsync(user);
                var userRole = await _userManager.GetRolesAsync(user);

            var model = new EditUserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Claims = userClaims.Select(x => x.Value).ToList(),
                Roles = userRole.ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.Where(x => x.Id == model.Id).FirstOrDefault();
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with id {model.Id } could not be found";
                    return View("NotFound");
                }
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.UserName;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ListUser));

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user==null)
            {
                ViewBag.ErrorMessage = $" User id {id} could not be found";
                return View("NotFound");
            }
            else
            {
                try
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListUser", "Administration");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return RedirectToAction("ListUser", "Administration");
                }
                catch(Exception  ex)
                {
                    ViewBag.ErrorTitle = $"'{user.UserName}' is In Role..";
                    ViewBag.ErrorMessage = $"'{user.UserName}' is In Role . Please Delete Role Of {user.UserName}" +
                        $" And Try Again";
                    return View("Error");
                }   
            }
        }
    }
}
