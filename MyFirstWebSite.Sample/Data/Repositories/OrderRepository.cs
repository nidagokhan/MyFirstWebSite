using MyFirstWebSite.Sample.Data.Context;
using MyFirstWebSite.Sample.Data.Interfaces;
using MyFirstWebSite.Sample.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Data.Repositories
{
    public class OrderRepository : IOrderRepo
    {
        private readonly AppDbContext _context;
        private readonly ShoppingCart _scart;

        public OrderRepository(AppDbContext context, ShoppingCart scart)
        {
            _context=context;
            _scart=scart;
        }

        //todo order eklerken foreign keyden kaynaklı hata veriyor
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _context.Orders.Add(order);

            var shoppingCartItems = _scart.ShoppingCartItems;

            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = item.Amount,
                    DrinkID = item.Drink.DrinkID,
                    OrderID = order.OrderID,
                    Price = item.Drink.Price
                };

                _context.OrderDetails.Add(orderDetail);                
            }
            _context.SaveChanges();
        }
    }
}
