using Ordering.Domain.Interfaces;
using Ordering.Domain.Models;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Repositories;
using Xunit;

namespace Ordering.UnitTests
{
    public class BuyerRepositoryTest
    {
        [Fact]
        public void Inster_Buyer()
        {
            var factory = new OrderingContextDesignFactory();
            var context = factory.CreateDbContext(null);

            IBuyerRepository repository = new BuyerRepository(context);


            var id = LinFx.Utils.IDUtils.CreateNewId();
            repository.Add(new Buyer($"{id}", $"name:{id}"));

            var result = repository.UnitOfWork.SaveChangesAsync().Result;

            Assert.Equal(1, result);
        }
    }
}
