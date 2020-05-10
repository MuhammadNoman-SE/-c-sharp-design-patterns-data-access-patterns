using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyShop.Domain.Models;
using MyShop.Infrastructure;
using MyShop.Infrastructure.Repositories;

namespace MyShop.Web.Controllers
{
    public class CustomerController : Controller
    {
        private IRepository<Customer> cr;

        public CustomerController(IRepository<Customer> _cr)
        {
            cr = _cr;
        }

        public IActionResult Index(Guid? id)
        {
            if (id == null)
            {
                var customers = cr.All();

                return View(customers);
            }
            else
            {
                var customer = cr.Get(id.Value);

                return View(new[] { customer });
            }
        }
    }
}
