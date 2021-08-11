using KKTraders.Data;
using KKTraders.Extensions;
using KKTraders.Models;
using KKTraders.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaticDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Controllers
{
    public class UserOrderController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserOrderController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            orderVm = new OrderSummaryViewModel()
            {
                OrderHeader = new OrderHeader(),
                ProductList = new List<Data.Product>()
            };
        }
        [BindProperty]
        public OrderSummaryViewModel orderVm { get; set; }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OrderSummary()
        {
            List<Item> itemList = HttpContext.Session.GetObject<List<Item>>(SD.sessionCart);
            if(itemList !=null)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    orderVm.ProductList.Add(_context.Product.Where(x => x.Id == itemList[i].product.Id).FirstOrDefault());
                }
                return View(orderVm);
            }
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> OrderSummary(OrderSummaryViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            var userOrder = _context.OrderDetails.Where(x => x.OrderHeader.Email == user.ToString()).Include(y => y.OrderHeader).ToList();
            List<Item> itemList = new List<Item>();

            itemList = HttpContext.Session.GetObject<List<Item>>(SD.sessionCart);
            if (itemList.Count > 0)
            {
                orderVm.ProductList = new List<Data.Product>();
                for (int i = 0; i < itemList.Count; i++)
                {
                    orderVm.ProductList.Add(_context.Product.Where(x => x.Id == itemList[i].product.Id).FirstOrDefault());
                }

                if (!ModelState.IsValid)
                {

                    return View(model);
                }
                else
                {
                    if (userOrder.Count()==0)
                    {
                        orderVm.OrderHeader.OrderDate = DateTime.Now;
                        orderVm.OrderHeader.Name = model.OrderHeader.Name;
                        orderVm.OrderHeader.Phone = model.OrderHeader.Phone;
                        orderVm.OrderHeader.Status = SD.StatusSubmitted;
                        orderVm.OrderHeader.Email = model.OrderHeader.Email;
                        orderVm.OrderHeader.Address = model.OrderHeader.Address;
                        orderVm.OrderHeader.City = model.OrderHeader.City;
                        _context.orderHeader.Add(orderVm.OrderHeader);
                        _context.SaveChanges();
                    }

                }
                if (userOrder.Count() > 0)
                {
 
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        for (int j = 0; j < userOrder.Count; j++)
                        {
                            if (itemList[i].product.Id == userOrder[j].ProductId)
                            {
                                var orderDetail = _context.OrderDetails.Where(x => x.ProductId==userOrder[j].ProductId).FirstOrDefault();

                                orderDetail.Quantity = userOrder[j].Quantity + itemList[i].Quantity;

                                _context.OrderDetails.Update(orderDetail);
                                _context.SaveChanges();
                            }   
                        }
                        var dbProduct = _context.OrderDetails.Where(x => x.OrderHeaderId == userOrder[0].OrderHeaderId).Select(y => y.ProductId).ToList();
                        if (!dbProduct.Contains(itemList[i].product.Id))
                        {
                           OrderDetails   orderDetails = new OrderDetails()
                            {
                                ProductId = itemList[i].product.Id,
                                Price = itemList[i].product.Price,
                                ProductName = itemList[i].product.Name,
                                OrderHeaderId = userOrder[0].OrderHeaderId,
                                Quantity = itemList[i].Quantity,
                                discount=itemList[i].product.Discount

                            };
                            _context.OrderDetails.Add(orderDetails);
                            _context.SaveChanges();
                            

                        }


                    }

                }
                else
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        OrderDetails orderDetails = new OrderDetails()
                        {

                            ProductId = itemList[i].product.Id,
                            Price = itemList[i].product.Price,
                            ProductName = itemList[i].product.Name,
                            OrderHeaderId = orderVm.OrderHeader.Id,
                            Quantity = itemList[i].Quantity,
                            discount = itemList[i].product.Discount

                        };
                        _context.OrderDetails.Add(orderDetails);
                    }
                }
                HttpContext.Session.Clear();
                _context.SaveChanges();
                ViewBag.Title = $"Order Successfull";
                ViewBag.Message = $"Your request was sucessfully posted. Thank you for" +
                    $"choosing our service. Please contact our store for more information";
                return View("ConfirmRequest");
            }
            else
            {
                ViewBag.MessageHeader = "Nothing is added to the shoppinig cart";
                ViewBag.ErrorMessage = "Add Some Product and make a request.";
                return View("NotFound");
            }
        }


        public  int orderHeaderExist()
        {
            var user =    _userManager.GetUserAsync(User);

            var userOrder = _context.OrderDetails.Where(x => x.OrderHeader.Email == user.ToString()).Include(y => y.OrderHeader).ToList();
            if (userOrder.Count() >0)
            {
                return 1;
            }
            return 0;
        }
    }
}
