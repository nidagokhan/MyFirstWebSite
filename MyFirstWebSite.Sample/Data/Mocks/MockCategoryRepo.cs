using MyFirstWebSite.Sample.Data.Interfaces;
using MyFirstWebSite.Sample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Data.Mocks
{
    public class MockCategoryRepo : ICategoryRepo
    {
        public IEnumerable<Category> Categories
        {
            get
            {
                return new List<Category>
                {
                    new Category { CategoryName = "Alcoholic", Description = "All alcoholic drinks" },
                    new Category { CategoryName = "Non-alcoholic", Description = "All non-alcoholic drinks" }
                };
            }
        }
    }
}
