using MyShop.Domain.Models;
using MyShop.Infrastructure.Lazy.Ghost;
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
        public CustomerRepository(ShoppingContext c) : base(c) { }

        public override Customer Update(Customer entity)
        {
            Customer oc = c.Customers.Single(cm => cm.CustomerId == entity.CustomerId);
            oc.Name = entity.Name;
            oc.ShippingAddress = entity.ShippingAddress;
            oc.City = entity.City;
            oc.PostalCode = entity.PostalCode;
            oc.Country = entity.Country;
            return base.Update(oc);
        }
        public override IEnumerable<Customer> All()
        {
            return base.All().Select(MapCustomer);
            //return base.All().Select(c =>
            //{
            //    c.ProilePictureHolder = new Lazy<byte[]>(() =>
            //    {
            //        return ProfilePictureService.GetFor(c.Name);
            //    });
            //    return c;
            //});
        }

        private CustomerProxy MapCustomer(Customer entity) {
            var customer = c.Customers.Where(c => c.CustomerId == entity.CustomerId).Single();

            return new CustomerGhost(() => base.Get(entity.CustomerId)) {
                CustomerId = entity.CustomerId
            };

            //CustomerProxy cp = new CustomerProxy
            //{
            //    Name = entity.Name,
            //    ShippingAddress = entity.ShippingAddress,
            //    City = entity.City,
            //    PostalCode = entity.PostalCode,
            //    Country = entity.Country
            //};
            //return cp;
        }
    }
}
