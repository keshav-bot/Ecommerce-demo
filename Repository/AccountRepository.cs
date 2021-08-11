using KKTraders.Models;
using KKTraders.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Repository
{

    public class AccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _service;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,IConfiguration service, RoleManager<IdentityRole> roleManager )
        {
            _userManager = userManager;
             _signInManager = signInManager;
            _emailService = emailService;
            _service = service;
            _roleManager = roleManager;
        }

        public SignInManager<ApplicationUser> SignInManager { get; }

        public async Task<IdentityResult> CreateUserAsync(SignUpModel userModel)
        {
            var user = new ApplicationUser() {
              Email =userModel.Email,
              UserName=userModel.Email,
              FirstName=userModel.FirstName,
              LastName=userModel.LastName
             
            };
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if(result.Succeeded)
            {
                await GenerateEmailConfirmationToken(user);
            }
            return result;

        }
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {

            return  await _userManager.FindByEmailAsync(email);
        }

       
        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
             var result =await _signInManager.PasswordSignInAsync(signInModel.Email,signInModel.Password,signInModel.RememberMe,false);
            return result;
        }
        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<List<ApplicationUser>> ListUser()
        {
            var users =  await _userManager.Users.ToListAsync();
            return users;

        }
        public async Task GenerateEmailConfirmationToken(ApplicationUser user)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (token != null)
            {
                await SendEmailConfirmationEmail(user, token);
            }
        }
        private async Task SendEmailConfirmationEmail(ApplicationUser user ,string token)
        {
            string appDomain = _service.GetSection("EmailConfirm:appDomain").Value;
            string confirmationLink = _service.GetSection("EmailConfirm:EmailConfirmation").Value;
            UserEmailOption options = new UserEmailOption()
            {
              
                ToEmails = new List<string> { user.Email },
                Placeholder = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string,string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string,string>("{{Link}}", string.Format(appDomain + confirmationLink, user.Id, token))

                }
            };
           await  _emailService.SendConfirmationMessage(options);
        }
        public  async Task DeleteUser(string userId)
        {
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                //var userInRole = await _roleManager.FindByIdAsync(userId);

                await _userManager.DeleteAsync(user);
            }

        }


    }

}
