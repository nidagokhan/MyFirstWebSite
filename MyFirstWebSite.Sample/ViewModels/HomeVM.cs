using MyFirstWebSite.Sample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Drink> PreferredDrinks { get; set; }
    }
}
