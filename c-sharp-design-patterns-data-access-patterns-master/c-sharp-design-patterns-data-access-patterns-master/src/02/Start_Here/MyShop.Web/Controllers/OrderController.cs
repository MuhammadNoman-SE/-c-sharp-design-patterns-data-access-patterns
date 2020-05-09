using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Models;
using MyShop.Infrastructure;
using MyShop.Infrastructure.Repositories;
using MyShop.Web.Models;

namespace MyShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private IRepository<Order> _orderRepository;
        private IRepository<Product> _productRepository;
        private IRepository<Customer> _customerRepository;


        public OrderController(IRepository<Order> orderRepository, IRepository<Product> productRepository, IRepository<Customer> customerRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            var orders = _orderRepository.Find(order => order.OrderDate > DateTime.UtcNow.AddDays(-1)).ToList();

            return View(orders);
        }

        public IActionResult Create()
        {
            var products = _productRepository.All();

            return View(products);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderModel model)
        {
            if (!model.LineItems.Any()) return BadRequest("Please submit line items");

            if (string.IsNullOrWhiteSpace(model.Customer.Name)) return BadRequest("Customer needs a name");

            var customer= _customerRepository.Find(c=>c.Name == model.Customer.Name).FirstOrDefault();
            if (null != customer)
            {
                customer.Name = model.Customer.Name;
                customer.ShippingAddress = model.Customer.ShippingAddress;
                customer.City = model.Customer.City;
                customer.PostalCode = model.Customer.PostalCode;
                customer.Country = model.Customer.Country;
                _customerRepository.Update(customer);
                _customerRepository.SaveChanges();
            }
            else
            {
                customer = new Customer
                {
                    Name = model.Customer.Name,
                    ShippingAddress = model.Customer.ShippingAddress,
                    City = model.Customer.City,
                    PostalCode = model.Customer.PostalCode,
                    Country = model.Customer.Country
                };
            }
            var order = new Order
            {
                LineItems = model.LineItems
                    .Select(line => new LineItem { ProductId = line.ProductId, Quantity = line.Quantity })
                    .ToList(),

                Customer = customer
            };

            _orderRepository.Add(order);

            _orderRepository.SaveChanges();

            return Ok("Order Created");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
