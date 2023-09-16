using MyFirstWebSite.Sample.Data.Context;
using MyFirstWebSite.Sample.Data.Interfaces;
using MyFirstWebSite.Sample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Data.Repositories
{
    public class CategoryRepository : ICategoryRepo
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> Categories => _context.Categories;
    }
}
