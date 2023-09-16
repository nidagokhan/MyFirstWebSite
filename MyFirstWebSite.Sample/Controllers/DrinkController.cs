using Microsoft.AspNetCore.Mvc;
using MyFirstWebSite.Sample.Data.Interfaces;
using MyFirstWebSite.Sample.Data.Models;
using MyFirstWebSite.Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Controllers
{
    public class DrinkController : Controller
    {
        private readonly ICategoryRepo _cRepo;
        private readonly IDrinkRepo _dRepo;
        public DrinkController(ICategoryRepo cRepo, IDrinkRepo dRepo)
        {
            _cRepo = cRepo;
            _dRepo = dRepo;
        }
        public ViewResult List(string category)
        {
            //todo http://localhost:37577/Drink/List/Non-Alcoholic querystring de category ye göre sayfayı güncellemiyor
            string _category = category;
            IEnumerable<Drink> drinks;

            string currentCategory = string.Empty;

            if (string.IsNullOrEmpty(category))
            {
                drinks = _dRepo.Drinks.OrderBy(n => n.DrinkID);
                currentCategory = "All Drinks";
            }
            else
            {
                if (string.Equals("Alcoholic", _category, StringComparison.OrdinalIgnoreCase))              
                    drinks = _dRepo.Drinks.Where(p => p.Category.CategoryName.Equals("Alcoholic")).OrderBy(p=>p.Name);               
                else
                    drinks = _dRepo.Drinks.Where(p => p.Category.CategoryName.Equals("Non-alcoholic")).OrderBy(p => p.Name); 
                    
                currentCategory = _category;
            }

            return View(new DrinkListVM
            {
                Drinks = drinks,
                CurrentCategory = currentCategory
            });
        }
    }
}

