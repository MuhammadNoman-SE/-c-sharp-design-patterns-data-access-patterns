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
        private IUnitOfWork uw;

        public OrderController(IUnitOfWork _uw)
        {
            uw = _uw;
        }

        public IActionResult Index()
        {
            var orders = uw.or.Find(order => order.OrderDate > DateTime.UtcNow.AddDays(-1)).ToList();

            return View(orders);
        }

        public IActionResult Create()
        {
            var products = uw.pr.All();

            return View(products);
        }

        [HttpPost]
        public IActionResult Create(CreateOrderModel model)
        {
            if (!model.LineItems.Any()) return BadRequest("Please submit line items");

            if (string.IsNullOrWhiteSpace(model.Customer.Name)) return BadRequest("Customer needs a name");

            Customer ec = uw.cr.Find(c => c.Name == model.Customer.Name).FirstOrDefault();
            if (null != ec)
            {
                ec.Name = model.Customer.Name;
                ec.ShippingAddress = model.Customer.ShippingAddress;
                ec.City = model.Customer.City;
                ec.PostalCode = model.Customer.PostalCode;
                ec.Country = model.Customer.Country;
            }
            else
            {

                ec = new Customer
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

                Customer = ec
            };

            uw.or.Add(order);

            uw.or.SaveChanges();

            return Ok("Order Created");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
