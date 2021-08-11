using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KKTraders.Models;
using KKTraders.Models.ViewModel;

namespace KKTraders.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderHeader> orderHeader { get; set; }
        public DbSet<KKTraders.Models.SignUpModel> SignUpModel { get; set; }
        public DbSet<KKTraders.Models.SignInModel> SignInModel { get; set; }
        public DbSet<KKTraders.Models.ViewModel.ChangePasswordViewModel> ChangePasswordViewModel { get; set; }
        public DbSet<CartItem> ShoppingCartItem { get; set; }
        public DbSet<Notification> notification { get; set; }
        
        public DbSet<SalesReport> SalesReport { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        

        public DbSet<ProductInvoice> productInvoice { get; set; }



        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    foreach (var foreignkey in builder.Model.GetEntityTypes()
        //        .SelectMany(e => e.GetForeignKeys()))
        //    {
        //        foreignkey.DeleteBehavior = DeleteBehavior.Restrict;
        //    }
        //}
    }


   
}
