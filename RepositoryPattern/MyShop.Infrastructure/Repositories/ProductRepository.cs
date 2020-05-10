using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyShop.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(ShoppingContext _context) : base(_context) { }

        public override Product Update(Product pu) {
            Product op = _context.Products.Single(p => p.ProductId == pu.ProductId);
            op.Name = pu.Name;
            op.Price = pu.Price;
            return base.Update(op);
        }
    }
}
