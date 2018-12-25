using ShopFx.Basket.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopFx.Basket.Api.Services
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string customerId);
        IEnumerable<string> GetUsers();
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string id);
    }
}
