using KKTraders.Data;
using KKTraders.Models;
using KKTraders.Models.ViewModel;
using KKTraders.Repository;
using KKTraders.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace KKTraders.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly AccountRepository _accountRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public AccountController(AccountRepository accountRepository,
            RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, IUserService userService,
            ApplicationDbContext context,
            IEmailService emailService
            )
        {
            _accountRepository = accountRepository;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _userService = userService;
            _context = context;
            _emailService = emailService;
        }
        [Route("Sign_Up")]
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }
      
        [Route("Sign_Up")]
        [HttpPost]
        [AllowAnonymous]
        public  async Task<IActionResult> SignUP(SignUpModel userModel)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(userModel);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(userModel);
                }
                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new { email = userModel.Email });

            }
            return View(userModel);

             
        }
        [HttpGet]
        [AllowAnonymous]
        public  async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            EmailConfirmModel model = new EmailConfirmModel()
            {
                Email = email
            };
            if(!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                var Token = token.Replace(' ', '+');
                var user = await _userManager.FindByIdAsync(uid);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"Unable to find the {uid}";
                    return View("NotFound");
                }
                var result = await _userManager.ConfirmEmailAsync(user, Token);
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                    return View("AccountConfirmed");
                }
            }  
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel model)
        {
            var user =  await _accountRepository.GetUserByEmailAsync(model.Email);
            if(user!=null)
            {
                if(user.EmailConfirmed)
                {                  
                    model.EmailConfirmed = true;
                    return View(model);
                }
               await _accountRepository.GenerateEmailConfirmationToken(user);
                model.EmailSent = true;
                ModelState.Clear();
                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Ran into some unknown Error");

              
            }
            return View(model);
        }
        [Route("Login")]
        [AllowAnonymous]
        public  IActionResult Login()
        {
            return View();
        }
        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult>  Login(SignInModel model, String returnUrl="")
        {
            if(ModelState.IsValid)
            {
                var result =  await _accountRepository.PasswordSignInAsync(model);
                if(result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var role = await _userManager.GetRolesAsync(user);
                    if(role.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Admin", "Admin");
                    }
                    if(!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                     return RedirectToAction("Index","Home");
                }

                ModelState.AddModelError("", "Invalid credential");
            }
            return View(model);
        }
        [Route("Log-out")]
        [AllowAnonymous]
        public  async Task<IActionResult> LogOut()
        {
            HttpContext.Session.Clear();
            
            await _accountRepository.LogOutAsync();           
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }



        [HttpGet]
        [Route("Change_password")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Change_password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if(ModelState.IsValid)
            {

                var userId= _userService.getUserId();

                var user = await _userManager.FindByIdAsync(userId);

                var result =await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if(result.Succeeded)
                {
                    ViewBag.IsSucceed = true;
                    ModelState.Clear();
                    return View();
                }
            }
            
            return View(model);
            
           
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _accountRepository.DeleteUser(userId);
            return RedirectToAction("ListUsers", "Account");
        }
        [AllowAnonymous]
     
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public  async Task<IActionResult> ForgetPassword(ForgetPasswordModel model)
        {
            if(ModelState.IsValid)
            {
               var user= await _userManager.FindByEmailAsync(model.Email);
                var result = await _userManager.IsEmailConfirmedAsync(user);
                if (user!=null)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action(
                        "resetPassword","Account",
                        new {token=code, Email=model.Email},
                        protocol: Request.Scheme);
                    UserEmailOption options = new UserEmailOption()
                    {
                        ToEmails = new List<string> { model.Email },
                        Body = $"Please reset your password by <a href = '{HtmlEncoder.Default.Encode(callbackUrl)}'> clicking here </a>",
                        Subject = "Reset password ",
                    };
                    await _emailService.SendEmail(options);                
                }
               return View("ResetPasswordConfirmMessage");
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult resetPassword(string token , string email)
        {
            if(token==null || email==null)
            {
                ModelState.AddModelError("","operatioin cannot be updated");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public  async Task<IActionResult> resetPassword(resetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                return View("NotFound");
            }
            return View(model);
        }




    }
}
