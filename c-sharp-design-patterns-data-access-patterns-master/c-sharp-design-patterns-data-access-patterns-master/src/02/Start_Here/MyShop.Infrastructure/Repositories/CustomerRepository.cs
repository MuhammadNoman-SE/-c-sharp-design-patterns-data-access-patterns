using MyShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyShop.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(ShoppingContext _context) : base(_context) { }

        public override Customer Update(Customer cu) {
            Customer co = _context.Customers.Single(c => c.CustomerId == cu.CustomerId);

            co.Name = cu.Name;
            co.City = cu.City;
            co.Country = cu.Country;
            co.ShippingAddress = cu.ShippingAddress;
            co.PostalCode = cu.PostalCode;

            return base.Update(co);
        }
    }
}
