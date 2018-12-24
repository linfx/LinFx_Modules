using System.Threading.Tasks;

namespace ShopFx.Basket.Api.Models
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string customerId);
    }
}
