using KKTraders.Data;
using KKTraders.Models;
using KKTraders.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Repository
{
    public class OrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        public OrderRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager,IEmailService service)
        {
            _context = context;
            _emailService = service;
            _userManager = userManager;
        }
        public async Task<List<OrderDetails>> MyOrder(string user)
        {
            if (user != null)
            {
                var orderDetails =  await _context.OrderDetails.Where(x => x.OrderHeader.Email == user).Include(y => y.OrderHeader).ToListAsync();
                return orderDetails;
            }
            return null;
        }
        public async Task<OrderHeader> orderHeader(string user)
        {
            OrderHeader orderHeader = new OrderHeader();
            if(user!=null)
            {
                orderHeader = await _context.orderHeader.Where(x => x.Email == user).FirstOrDefaultAsync();
                return orderHeader;
            }
            return orderHeader;
        }


        public OrderDetails orderDetails( int id)
        {
            var orderDetails=_context.OrderDetails.Where(x => x.ProductId == id).FirstOrDefault();
            return orderDetails;

        }


        public async Task SendEmailConfirmationEmail(OrderConfirmEmail user)
        {
           
            OrderConfirmEmail options = new OrderConfirmEmail()
            {

                ToEmail = user.ToEmail
               
            };
            await _emailService.SendMessageAboutStatusAccepted(options);
        }

    }
}
