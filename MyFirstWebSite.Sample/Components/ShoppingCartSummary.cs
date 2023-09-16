using Microsoft.AspNetCore.Mvc;
using MyFirstWebSite.Sample.Data.Models;
using MyFirstWebSite.Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Components
{
    public class ShoppingCartSummary:ViewComponent
    {
        private readonly ShoppingCart _shoppingcart;
        public ShoppingCartSummary(ShoppingCart shoppingcart)
        {
            _shoppingcart=shoppingcart;
        }

        public IViewComponentResult Invoke()
        {
            var items = new List<ShoppingCartItem>() { new ShoppingCartItem(), new ShoppingCartItem() };//_shoppingcart.GetShoppingCartItems();
            _shoppingcart.ShoppingCartItems= items;

            var scVM=new ShoppingCartVM
            {
                ShoppingCart= _shoppingcart,
                ShoppingCartTotal=_shoppingcart.GetShoppingCartTotal()
            };

            return View(scVM);
        }
    }
}
