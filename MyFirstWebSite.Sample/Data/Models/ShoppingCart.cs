using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyFirstWebSite.Sample.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Data.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _context;
        public ShoppingCart(AppDbContext context)
        {
            _context=context;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            /*Dependency Injection(DI) kullanarak IHttpContextAccessor hizmetini alır ve bu hizmeti kullanarak HTTP isteğiyle ilişkilendirilen oturumu elde etmeye çalışır.Oturum, istemci tarafından sunucuya gönderilen verileri saklamak için kullanılır. Eğer oturum yoksa, HttpContext null olabilir, bu yüzden null koşul operatörü ?. ile kontrol edilir. */

            //todo alternatif olabilir mi bak
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();

            //?? operatörü, sol tarafındaki ifade null ise sağ tarafındaki ifadeyi kullanır. Yani bu satırda, eğer "CartId" oturum içinde mevcut değilse veya null ise, yeni bir benzersiz kimlik oluşturulur ve cartId değişkenine atanır. Eğer "CartId" oturum içinde mevcutsa ve null değilse, oturumdan alınan değer cartId değişkenine atanır. Bu şekilde cartId, eğer daha önce bir alışveriş sepeti kimliği oluşturulmamışsa veya oturumda saklanmamışsa, yeni bir benzersiz kimlikle başlatılır.Eğer daha önce bir kimlik oluşturulmuş ve oturumda saklanmışsa, bu kimlik tekrar kullanılır. Bu, her kullanıcı için benzersiz bir alışveriş sepeti kimliği sağlar.

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context)
            {
                ShoppingCartId= cartId
            };
        }

        public void AddToCart(Drink drink,int amount)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(a => a.Drink.DrinkID == drink.DrinkID && a.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem==null)
            {
                //daha önce sepette bu içecek yoksa miktarı 1 yap ve ekle
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId= ShoppingCartId,
                    Drink=drink,
                    Amount=1
                };               
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                //sepette varsa miktarı artır
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }

        public int RemoveFromChart(Drink drink)
        {
            var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(a => a.Drink.DrinkID == drink.DrinkID && a.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem!=null)
            {
                if (shoppingCartItem.Amount>1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges();
            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(a => a.ShoppingCartId == ShoppingCartId)
                .Include(a => a.Drink).ToList()); 
            
            //.Include(a => a.Drink): Bu, her alışveriş sepeti öğesinin bir içeceği (Drink) içermesini sağlar. Bu, ilişkili verileri çekmeyi ifade eder.
           
            //Sonuç olarak, eğer ShoppingCartItems null ise veya daha önce çekilmemişse, bu kod, belirli bir alışveriş sepetinin tüm öğelerini veritabanından çeker ve ShoppingCartItems değişkenine atar. Eğer ShoppingCartItems daha önce çekilmişse, veritabanına tekrar sorgu atmayı önler ve mevcut öğeleri geri döndürür. Bu, veritabanı erişimi gerektiren işlemleri optimize etmek için kullanılır.
        }

        public void ClearCart()
        {
            var cardItems = _context.ShoppingCartItems.Where(a => a.ShoppingCartId == ShoppingCartId);
            _context.ShoppingCartItems.RemoveRange(cardItems);
            _context.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                  .Select(c => c.Drink.Price * c.Amount).Sum();
            return total;
        }
    }
}
