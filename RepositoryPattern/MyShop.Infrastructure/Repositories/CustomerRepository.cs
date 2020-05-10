using MyShop.Domain.Lazy;
using MyShop.Domain.Models;
using MyShop.Infrastructure.Lazy.Proxy;
using MyShop.Infrastructure.sevices;
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

        //public override IEnumerable<Customer> All()
        //{
        //    return base.All().Select(c => {
        //        c.ProfilePictureValuHolder = new Lazy<byte[]>(() => {
        //            return ProfilePictureService.GetFor(c.Name);
        //        });
        //        return c;
        //    });


        //}

        public override IEnumerable<Customer> All() {
            return base.All().Select(MapCustomer);
        }
        private CustomerProxy MapCustomer(Customer cu) {
            CustomerProxy cp = new CustomerProxy {

            Name = cu.Name,
            City = cu.City,
            Country = cu.Country,
            ShippingAddress = cu.ShippingAddress,
            PostalCode = cu.PostalCode

        };
            return cp;
        }
    }
}
