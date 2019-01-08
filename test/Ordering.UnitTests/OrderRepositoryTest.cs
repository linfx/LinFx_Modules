using Ordering.Domain.Interfaces;
using Ordering.Domain.Models;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Repositories;
using System;
using Xunit;

namespace Ordering.UnitTests
{
    public class OrderRepositoryTest
    {
        [Fact]
        public void Inster_Order()
        {
            var factory = new OrderingContextDesignFactory();
            var context = factory.CreateDbContext(null);

            IOrderRepository repository = new OrderRepository(context);

            var address = new Address("Street", "深圳", "广东", "Country", "ZipCode");
            var order = new Order("7036600510103552", "UserName", address, 1, "message.CardNumber", "message.CardSecurityNumber", "message.CardHolderName", DateTime.Now);

            repository.Add(order);

            order.SetBuyerId(2);

            var result = repository.UnitOfWork.SaveChangesAsync().Result;

            Assert.Equal(1, result);
        }
    }
}
