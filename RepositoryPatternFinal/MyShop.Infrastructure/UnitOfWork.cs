using MyShop.Domain.Models;
using MyShop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure
{
    public interface IUnitOfWork
    {
        IRepository<Customer> cr { get; }
        IRepository<Product> pr { get; }
        IRepository<Order> or { get; }
        void SaveChanges();

    }
    public class UnitOfWork:IUnitOfWork
    {
        ShoppingContext c;
        public UnitOfWork(ShoppingContext _c ) {
            c = _c;
        }
        private IRepository<Customer> _cr;
        private IRepository<Product> _pr;
        private IRepository<Order> _or;

        public IRepository<Customer> cr {
            get {
                if (null == _cr)
                    _cr = new CustomerRepository(c);
                return _cr;
            }
        }
        public IRepository<Product> pr
        {
            get
            {
                if (null == _pr)
                    _pr = new ProductRepository(c);
                return _pr;
            }
        }
        public IRepository<Order> or
        {
            get
            {
                if (null == _or)
                    _or = new OrderRepository(c);
                return _or;
            }
        }
        public void SaveChanges() {
            c.SaveChanges();
        }


    }
}
