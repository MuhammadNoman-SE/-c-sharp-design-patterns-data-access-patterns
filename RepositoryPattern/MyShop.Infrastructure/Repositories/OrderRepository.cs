using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyShop.Infrastructure.Repositories
{
    public class OrderRepository: GenericRepository<Order>
    {
        public OrderRepository(ShoppingContext _context):base(_context) { }

        public override IEnumerable<Order> Find(Expression<Func<Order,bool>> predicate) {
            return _context.Orders
                .Include(o => o.LineItems)
                .ThenInclude(o => o.Product)
                .Where(predicate)
                .ToList();
        }

        public override Order Update(Order ou) {
            Order oo = _context.Orders
                    .Include(o => o.LineItems)
                    .ThenInclude(o => o.Product)
                    .Single(o=> o.OrderId == ou.OrderId);
            oo.LineItems = ou.LineItems;
            oo.OrderDate = ou.OrderDate;
            return base.Update(oo);
        }
    }
}
