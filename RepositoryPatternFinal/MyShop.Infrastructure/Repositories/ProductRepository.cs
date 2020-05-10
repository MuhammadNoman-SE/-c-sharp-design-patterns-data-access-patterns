using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyShop.Infrastructure.Repositories
{
    public class ProductRepository:GenericRepository<Product>
    {
        public ProductRepository(ShoppingContext c) : base(c){ }

        public override Product Update(Product entity)
        {
            Product op = c.Products.Single(p => p.ProductId == entity.ProductId);
            op.Name = entity.Name;
            op.Price = entity.Price;
            return base.Update(entity);
        }
    }
}
