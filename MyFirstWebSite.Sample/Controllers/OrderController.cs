using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebSite.Sample.Data.Interfaces;
using MyFirstWebSite.Sample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepo _orepo;
        private readonly ShoppingCart _scart;

        public OrderController(IOrderRepo orepo, ShoppingCart scart)
        {
            _orepo = orepo;
            _scart=scart;
        }

       //todo  authorize eklenince maxquerystring hatası veriyo
       
        // [Authorize]
     
        public IActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        //[Authorize]
        public ActionResult CheckOut(Order order)
        {
            var items = _scart.GetShoppingCartItems();
            _scart.ShoppingCartItems=items;

            if (_scart.ShoppingCartItems.Count==0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some drinks first");
            }

            if (ModelState.IsValid)
            {
                _orepo.CreateOrder(order);
                _scart.ClearCart();
                return RedirectToAction("CheckOutComplete");
            }
            return View(order);
        }

        public ActionResult CheckOutComplete()
        {
            ViewBag.CheckOutCompleteMessage = "Thanks for your order <3";
            return View();
        }
    }
}
