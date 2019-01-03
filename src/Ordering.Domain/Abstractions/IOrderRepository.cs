using LinFx.Domain;
using LinFx.Domain.Abstractions;
using Ordering.Domain.Models;
using System.Threading.Tasks;

namespace Ordering.Domain.Interfaces
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface IOrderRepository : IRepository<Order>
    {
        Order Add(Order order);

        void Update(Order order);

        Task<Order> GetAsync(int orderId);
    }
}
