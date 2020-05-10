using MyShop.Domain.Models;
using MyShop.Infrastructure.Lazy.Proxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure.Lazy.Ghost
{
    public class CustomerGhost : CustomerProxy
    {
        private LoadStatus s;
        private Func<Customer> load;

        public CustomerGhost(Func<Customer> l) {
            load = l;
            s = LoadStatus.GHOST;
        }

        public override string Name {
            get {
                loadDta();
                return base.Name;
            }
            set {
                loadDta();
                base.Name = value;
            }
        }

        private void loadDta() {

            if (s == LoadStatus.GHOST)
            {
                s = LoadStatus.LOADING;
                var customer = load();
                base.Name = customer.Name;
                base.ShippingAddress = customer.ShippingAddress;
                base.City = customer.City;
                base.PostalCode = customer.PostalCode;
                base.Country = customer.Country;

                s = LoadStatus.LOADED;
            }
        }
    }
    enum LoadStatus { GHOST, LOADING,LOADED};
}
