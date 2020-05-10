using MyShop.Domain.Models;
using MyShop.Infrastructure.Lazy.Proxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.Lazy.GhostObject
{
    class CustomerGhost:CustomerProxy
    {
        private LoadStatus status;
        private readonly Func<Customer> load;

        public CustomerGhost(Func<Customer> load) {
            this.load = load;
            status = LoadStatus.GHOST;
        }
        public override string Name {
            get {
                Load();
                return base.Name;

            }
            set {
                Load();
                base.Name = value;
            }
        }

        private void Load() {
            if (status == LoadStatus.GHOST)
            {
                status = LoadStatus.LOADING;
                var customer = load();
                base.Name = customer.Name;
                base.ShippingAddress = customer.ShippingAddress;
                base.City = customer.City;
                base.PostalCode = customer.PostalCode;
                base.Country = customer.Country;

                status = LoadStatus.LOADED;
            }

        }
    }
    enum LoadStatus  {GHOST, LOADING, LOADED};
}
