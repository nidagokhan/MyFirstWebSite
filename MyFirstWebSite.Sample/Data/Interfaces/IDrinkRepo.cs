using MyFirstWebSite.Sample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Data.Interfaces
{
    public interface IDrinkRepo
    {
        IEnumerable<Drink> Drinks { get;  }

        IEnumerable<Drink> PreferredDrink { get; }
        Drink GetDrinkById(int drinkId);
    }
}
