using Microsoft.EntityFrameworkCore;
using MyFirstWebSite.Sample.Data.Context;
using MyFirstWebSite.Sample.Data.Interfaces;
using MyFirstWebSite.Sample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Data.Repositories
{
    public class DrinkRepository : IDrinkRepo
    {
        private readonly AppDbContext _context;
        public DrinkRepository(AppDbContext context)
        {
            _context= context;
        }
        public IEnumerable<Drink> Drinks => _context.Drinks.Include(c => c.Category);

        public IEnumerable<Drink> PreferredDrink => _context.Drinks.Where(p => p.IsPreferredDrink).Include(c => c.Category);

        public Drink GetDrinkById(int drinkId) => _context.Drinks.FirstOrDefault(p => p.DrinkID == drinkId);
    }
}
