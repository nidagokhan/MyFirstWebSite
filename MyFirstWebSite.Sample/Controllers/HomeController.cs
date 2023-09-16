using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFirstWebSite.Sample.Data.Interfaces;
using MyFirstWebSite.Sample.Models;
using MyFirstWebSite.Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDrinkRepo _drepo;
        public HomeController(ILogger<HomeController> logger,IDrinkRepo drepo)
        {
            _drepo= drepo;
            _logger = logger;
        }

        public ViewResult Index()
        {
            var homeVM = new HomeVM
            {
                PreferredDrinks = _drepo.PreferredDrink
            };

            return View(homeVM);
        }

        #region Default
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        } 
        #endregion
    }
}
