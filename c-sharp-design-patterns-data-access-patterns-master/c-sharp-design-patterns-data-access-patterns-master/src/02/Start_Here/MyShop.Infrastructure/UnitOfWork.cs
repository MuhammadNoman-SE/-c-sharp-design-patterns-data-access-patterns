using MyShop.Domain.Models;
using MyShop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Infrastructure
{
    public interface IUnitOfWork 
    {
        IRepository<Customer> CustomerRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<Product> ProductRepository { get; }
        void SaveChanges();


    }
    public class UnitOfWork:IUnitOfWork
    {
        private ShoppingContext _context;
        public UnitOfWork(ShoppingContext context) {
            _context = context;
        }

        private IRepository<Customer> _customerRepository;
        private IRepository<Order> _orderRepository;
        private IRepository<Product> _productRepository;

        public IRepository<Customer> CustomerRepository { 
            get {
                if (null == _customerRepository)
                    _customerRepository = new CustomerRepository(_context);
                return _customerRepository;
            } 
        }
        public IRepository<Order> OrderRepository       { get {
                if (null == _orderRepository)
                    _orderRepository = new OrderRepository(_context);
                return _orderRepository;

            } }
        public IRepository<Product> ProductRepository   { get {
                if (null == _productRepository)
                    _productRepository = new ProductRepository(_context);
                return _productRepository;
            } }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
