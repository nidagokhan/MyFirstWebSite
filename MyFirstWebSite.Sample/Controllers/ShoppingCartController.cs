using Microsoft.AspNetCore.Mvc;
using MyFirstWebSite.Sample.Data.Interfaces;
using MyFirstWebSite.Sample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFirstWebSite.Sample.ViewModels;

namespace MyFirstWebSite.Sample.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IDrinkRepo _drepo;
        private readonly ShoppingCart _cart;

        public ShoppingCartController(IDrinkRepo drepo, ShoppingCart cart)
        {
            _drepo = drepo;
            _cart=cart;
        }
        public ViewResult Index()
        {
            var items = _cart.GetShoppingCartItems();
            _cart.ShoppingCartItems=items;

            var scVM = new ShoppingCartVM
            {
                ShoppingCart = _cart,
                ShoppingCartTotal = _cart.GetShoppingCartTotal()
            };

            return View(scVM);
        }

        public RedirectToActionResult AddToShoppingCart(int drinkId)
        {
            var selectedDrink = _drepo.Drinks.FirstOrDefault(a => a.DrinkID == drinkId);
            if (selectedDrink!=null)
            {
                _cart.AddToCart(selectedDrink, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int drinkId)
        {
            var seletedDrink = _drepo.Drinks.FirstOrDefault(a => a.DrinkID == drinkId);
            if (seletedDrink!=null)
            {
                _cart.RemoveFromChart(seletedDrink);
            }
            return RedirectToAction("Index");
        }
    }
}
