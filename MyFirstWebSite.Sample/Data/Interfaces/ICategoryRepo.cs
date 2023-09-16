using MyFirstWebSite.Sample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Data.Interfaces
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> Categories { get; }
    }
}
