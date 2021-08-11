using KKTraders.Data;
using KKTraders.Models;
using KKTraders.Models.ViewModel;
using KKTraders.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Controllers
{
    
    public class Order : Controller
    {
        private readonly OrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public Order(OrderRepository orderRepository,
                      UserManager<ApplicationUser> userManager,ApplicationDbContext context)
        {
            _context = context;
            _orderRepository = orderRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> MyOrder()
        {
            var user =  await _userManager.GetUserAsync(User);
            OrderHeader orderHeader = await _orderRepository.orderHeader(user.ToString());
            List<OrderDetails> orderDetails = await _orderRepository.MyOrder(user.ToString());
            OrderHeaderOrderDetailsViewModel model = new OrderHeaderOrderDetailsViewModel() 
            {
                orderDetails=new List<OrderDetails>()
            };
            if(orderHeader !=null)
            {
                model.Id = orderHeader.Id;
                model.Name = orderHeader.Name;
                model.Email = orderHeader.Email;
                model.Phone = orderHeader.Phone;
                model.Address = orderHeader.Address;
                model.City = orderHeader.City;
            }
            if(orderDetails !=null)
            {
                foreach (var item in orderDetails)
                {
                 
                    model.orderDetails.Add(new OrderDetails()
                    {
                        ProductName=item.ProductName,
                        Price=item.Price,
                        Quantity=item.Quantity,   
                        ProductId=item.ProductId
                    });
                }
            }
            return View(model);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public  async Task<IActionResult> UpdateOrderInformation(OrderHeaderOrderDetailsViewModel model)
        {
            OrderHeaderOrderDetailsViewModel model1 = new OrderHeaderOrderDetailsViewModel()
            {

                orderDetails = new List<OrderDetails>()
            };
            if (ModelState.IsValid)
            {
                var userInfo = await _orderRepository.orderHeader(model.Email);
                List<OrderDetails> orderDetails = await _orderRepository.MyOrder(model.Email);
                if (userInfo != null)
                {
                    userInfo.Email = model.Email;
                    userInfo.Phone = model.Phone;
                    userInfo.Name = model.Name;
                    userInfo.Address = model.Address;
                    _context.orderHeader.Update(userInfo);
                    _context.SaveChanges();
                }
                OrderHeader orderHeader = await _orderRepository.orderHeader(model.Email);
                if (orderHeader != null)
                {
                    model1.Id = orderHeader.Id;
                    model1.Name = orderHeader.Name;
                    model1.Email = orderHeader.Email;
                    model1.Phone = orderHeader.Phone;
                    model1.Address = orderHeader.Address;
                    model1.City = orderHeader.City;
                }
                if (orderDetails != null)
                {
                    foreach (var item in orderDetails)
                    {

                        model1.orderDetails.Add(new OrderDetails()
                        {
                            ProductName = item.ProductName,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            ProductId = item.ProductId
                        });
                    }
                }

                return View("MyOrder", model1);
            }

            return View("MyOrder", model1);
    
        }
        
        public IActionResult RemoveMyOrder(int orderId)
        {
           var result= _orderRepository.orderDetails(orderId);
            _context.OrderDetails.Remove(result);
            _context.SaveChanges();
            return RedirectToAction(nameof(MyOrder));

        }
    }
}
