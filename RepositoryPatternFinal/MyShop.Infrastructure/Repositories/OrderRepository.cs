using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyShop.Infrastructure.Repositories
{
    public class OrderRepository: GenericRepository<Order>
    {
        public OrderRepository(ShoppingContext c):base(c) { }

        public override IEnumerable<Order> Find(Expression<Func<Order, bool>> e)
        {
            return c.Orders
                .Include(order => order.LineItems)
                .ThenInclude(lineItem => lineItem.Product)
                .Where(e).ToList();
        }

        public override Order Update(Order entity)
        {
            Order oo = c.Orders
                    .Include(o => o.LineItems)
                    .ThenInclude(o => o.Product)
                    .Single(o => o.OrderId == entity.OrderId);
            oo.LineItems = entity.LineItems;
            oo.OrderDate = entity.OrderDate;
            return base.Update(entity);
        }
    }
}
