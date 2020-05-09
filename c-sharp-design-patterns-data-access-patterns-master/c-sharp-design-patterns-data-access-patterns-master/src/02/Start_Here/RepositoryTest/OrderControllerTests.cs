using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyShop.Domain.Models;
using MyShop.Infrastructure;
using MyShop.Infrastructure.Repositories;
using MyShop.Web.Controllers;
using MyShop.Web.Models;
using System;
using System.Collections.Generic;

namespace RepositoryTest
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var orderRepositoryMoq = new Mock<IRepository<Order>>();
            var productRepositoryMoq = new Mock<IRepository<Product>>();
            var customerRepositoryMoq = new Mock<IRepository<Customer>>();

            var unitOfWorkMoq = new Mock<IUnitOfWork>();
            //unitOfWorkMoq.Setup(It.IsAny<ShoppingContext>());

            var orderController = new OrderController(unitOfWorkMoq.Object);
            IEnumerable<LineItemModel> lineItems = new[] {
                new LineItemModel { ProductId = Guid.NewGuid(), Quantity = 2 },
                new LineItemModel { ProductId = Guid.NewGuid(), Quantity = 4 }
            };

            CustomerModel customer = new CustomerModel
            {
                Name = "nm",
                ShippingAddress = "sa",
                City = "cty",
                PostalCode = "pc",
                Country = "c"
            };

            var createOrderModel = new CreateOrderModel()
            {
                LineItems = lineItems,
                Customer = customer
            };

            orderController.Create(createOrderModel);

            orderRepositoryMoq.Verify(or => or.Add(It.IsAny<Order>()), Times.Once);
        }
    }
}
