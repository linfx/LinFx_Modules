using Ordering.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.Application.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderAsync(int id);

        Task<IEnumerable<OrderSummary>> GetOrdersFromUserAsync(string userId);

        Task<IEnumerable<CardType>> GetCardTypesAsync();
    }
}
