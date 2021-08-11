using KKTraders.Data;
using KKTraders.Extensions;
using KKTraders.Models;
using KKTraders.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaticDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KKTraders.Controllers
{
   
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public string ShoppingCartId { get; set; }
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            if (HttpContext.Session.GetObject<List<Item>>(SD.sessionCart) == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item {product = await _context.Product.Where(x=>x.Id==id).FirstOrDefaultAsync() , Quantity = 1 });
                HttpContext.Session.SetObject(SD.sessionCart, cart);

            }
            else
            {
                List<Item> cart = HttpContext.Session.GetObject<List<Item>>(SD.sessionCart);
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { product = _context.Product.Where(x=>x.Id==id).FirstOrDefault(), Quantity = 1 });
                }
                HttpContext.Session.SetObject(SD.sessionCart,cart);
            }
            var count = HttpContext.Session.GetObject<List<Item>>(SD.sessionCart).Count;

            return Json(count);

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ShoppingCart()
        {
            List<Item> itemList = new List<Item>();
            itemList = HttpContext.Session.GetObject<List<Item>>(SD.sessionCart);
            return View(itemList);  
        }
        [AllowAnonymous]
    
        public IActionResult DeleteCartItem(int  id)
        {

             var product = _context.Product.Where(x => x.Id == id).FirstOrDefault();
            List<Item> itemList = new List<Item>();
                 itemList = HttpContext.Session.GetObject<List<Item>>(SD.sessionCart);
                for(int i=0;i< itemList.Count;i++)
                {
                    Item item = new Item()
                    {
                        product = itemList[i].product,
                        Quantity = itemList[i].Quantity,
                    };
                    if(itemList[i].product.Id==id)
                    {
                        itemList.RemoveAt(i);
                        
                    }
                 HttpContext.Session.SetObject(SD.sessionCart, itemList);
                }

            return RedirectToAction(nameof(ShoppingCart));
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult updatecart(int id , int Quantity )
       {

            List<Item> itemList = new List<Item>();
            itemList = HttpContext.Session.GetObject<List<Item>>(SD.sessionCart);

            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].product.Id.Equals(id))
                {
                    itemList[i].Quantity=Quantity;
                }
                HttpContext.Session.SetObject(SD.sessionCart, itemList);
            }
          
            return RedirectToAction(nameof(ShoppingCart));
        }

        private int isExist(int id)
        {
            List<Item> cart = HttpContext.Session.GetObject<List<Item>>(SD.sessionCart);
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

