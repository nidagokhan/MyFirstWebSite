using Microsoft.AspNetCore.Mvc;
using MyFirstWebSite.Sample.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Components
{
    public class CategoryMenu:ViewComponent
    {
        private readonly ICategoryRepo _crepo;
        public CategoryMenu(ICategoryRepo crepo)
        {
            _crepo = crepo;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _crepo.Categories.OrderBy(c => c.CategoryName);
            return View(categories);
        }
    }
}
