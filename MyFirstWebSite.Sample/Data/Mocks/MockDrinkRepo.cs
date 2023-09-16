using MyFirstWebSite.Sample.Data.Interfaces;
using MyFirstWebSite.Sample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Data.Mocks
{
    public class MockDrinkRepo : IDrinkRepo
    {
        private readonly ICategoryRepo _cRepo;
        public MockDrinkRepo(ICategoryRepo cRepo)
        {
            _cRepo=cRepo;
        }
        public IEnumerable<Drink> Drinks {
            get
            {
                return new List<Drink>
                {
                    new Drink
                    {
                        Name="Bira",
                        Price=7.95M,
                        ShortDescription="En çok tüketilen alkol",
                        LongDescription="...",
                        Category=_cRepo.Categories.First(),
                        //ImagePath  = "",
                        InStock=100,
                        IsPreferredDrink=true,
                        ImageThumbnailPath ="~/Images/bira.jpg"
                    },
                    new Drink {
                        Name = "Rum & Kola",
                        Price = 12.95M, ShortDescription = "Koladan yapılan rum ve limon içeren kokteyl",
                        LongDescription = "...",
                        Category =  _cRepo.Categories.First(),
                        //ImagePath  = "",
                        InStock = 689,
                        IsPreferredDrink = false,
                        ImageThumbnailPath  = "~/Images/RumKola.jpg"
                    },
                    new Drink {
                        Name = "Tekila",
                        Price = 12.95M, ShortDescription = "Mavi agav bitkisinden yapılan içecek.",
                        LongDescription = "...",
                        Category =  _cRepo.Categories.First(),
                        //ImagePath  = "",
                        InStock = 500,
                        IsPreferredDrink = false,
                        ImageThumbnailPath  = "~/Images/Tekila.jpg"
                    },
                    new Drink
                    {
                        Name = "Meyve Suyu",
                        Price = 12.95M,
                        ShortDescription = "Doğal meyve ve sebze suyu",
                        LongDescription = "...",
                        Category = _cRepo.Categories.Last(),
                        //ImagePath  = "",
                        InStock = 150,
                        IsPreferredDrink = false,
                        ImageThumbnailPath  = "~/Images/MeyveSuyu.png"
                    }
                };
            }
        }
        public IEnumerable<Drink> PreferredDrink { get ; }

        public Drink GetDrinkById(int drinkId)
        {
            throw new NotImplementedException();
        }
    }
}
