using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Data.Models
{
    public class Drink
    {
        public int DrinkID { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal Price { get; set; }
        //public string ImagePath { get; set; }
        public string ImageThumbnailPath { get; set; }
        public bool IsPreferredDrink { get; set; }
        public int InStock { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}
