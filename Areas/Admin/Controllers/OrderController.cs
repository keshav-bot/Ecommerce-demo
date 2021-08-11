using KKTraders.Data;
using KKTraders.Extensions;
using KKTraders.Models;
using KKTraders.Models.ViewModel;
using KKTraders.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaticDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly OrderRepository _orderRepository;
        [BindProperty]
        public OrderSummaryViewModel orderVm { get; set; }
        public OrderController(ApplicationDbContext context,OrderRepository orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
            orderVm = new OrderSummaryViewModel()
            {
                OrderHeader = new OrderHeader(),
                ProductList = new List<Data.Product>()
            };
        }
        public IActionResult RequestStatus(string status)
        {

            if (status == "All")
            {
                var allOrder = _context.orderHeader.ToList();
                return View(allOrder);
            }
            else if (status == "Approved")
            {
                var requestStatus = _context.orderHeader.Where(x => x.Status == "Approved").ToList();

                return View(requestStatus);
            }
            else if (status == "Pending")
            {
                var requestStatus = _context.orderHeader.Where(x => x.Status == "Pending").ToList();
                return View(requestStatus);
            }
            else if (status == "Rejected")
            {
                var requestStatus = _context.orderHeader.Where(x => x.Status == "Rejected").ToList();
                return View(requestStatus);
            }
            else
            {
                return View();
            }
        } 
        [HttpGet]
        public async Task<IActionResult> Dispatch(int id)
        {
            var orderheader = _context.orderHeader.Where(x => x.Id == id).FirstOrDefault();
            Random random = new Random();
            int num = random.Next(101, 1000000);
            SalesReport invoice = new SalesReport()
            {
                NameOfCustomer = orderheader.Name,
                SalesId = num.ToString(),
                Email = orderheader.Email,
                DateOfSale = DateTime.Now.Date
            };
            _context.SalesReport.Add(invoice);

            _context.SaveChanges();

            var orderDatails = _context.OrderDetails.Where(x => x.OrderHeaderId == id).ToList();
            List<ProductInvoice> productInvoice = new List<ProductInvoice>();
            foreach (var item in orderDatails)
            {
                _context.productInvoice.Add(new ProductInvoice()
                {
                    Name = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    SalesReportId=invoice.Id,
                    
                });
                var product=_context.Product.Where(x => x.Name == item.ProductName).FirstOrDefault();
                product.Units = product.Units - item.Quantity;
                _context.OrderDetails.Remove(item);
            }
            _context.orderHeader.Remove(orderheader);

            //code for sending email to User... about order is confirmed
            OrderConfirmEmail m = new OrderConfirmEmail()
            {
                ToEmail = orderheader.Email
            };
             await _orderRepository.SendEmailConfirmationEmail(m);
            //end
            _context.SaveChanges();
            return View("RequestStatus") ;

        }
        public IActionResult OrderDetails(int OrderHeaderId)
        {
            OrderViewModel ordervm = new OrderViewModel()
            {
                orderHeader = _context.orderHeader.Where(x => x.Id == OrderHeaderId).FirstOrDefault(),
                orderDetails = _context.OrderDetails.Where(x => x.OrderHeader.Id == OrderHeaderId).ToList()
            };
            return View(ordervm);
        }
        public IActionResult ChangeOrderStatus(int id , string status)
        {
            var order= _context.OrderDetails.Where(x => x.Id == id).Include(x=>x.OrderHeader).FirstOrDefault();

            if(order ==null)
            {
                return View("NotFound");
            }
            order.OrderHeader.Status = status;
            _context.SaveChanges();
            return RedirectToAction("RequestStatus",new { status =status});
        }
        public IActionResult Delete(int id )
        {
            var order = _context.orderHeader.Where(x => x.Id == id).FirstOrDefault();
            _context.Remove(order);
            _context.SaveChanges();
            return RedirectToAction("RequestStatus", new { status = "Rejected" });
        }
       
    }
}
