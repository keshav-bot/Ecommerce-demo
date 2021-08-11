using KKTraders.Data;
using KKTraders.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KKTraders.Initializer
{
    public class DbInitializer:IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex)
            {

            }
            if (_db.Roles.Any(x => x.Name == "Admin")) return;

            _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            _userManager.CreateAsync(new ApplicationUser()
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                EmailConfirmed = true,
                FirstName = "Keshav",
                LastName = "Koirala",

            }, "password").GetAwaiter().GetResult();

             ApplicationUser user = _userManager.Users.Where(x => x.Email == "Admin@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult(); 
         
           


        }
    }
}
