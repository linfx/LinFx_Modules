using LinFx.Utils;
using Microsoft.eShopOnContainers.WebMVC.ViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Infrastructure;
using WebMVC.Models;

namespace Microsoft.eShopOnContainers.WebMVC.Services
{
    public class BasketService : IBasketService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _httpClient;
        private readonly string _basketByPassUrl;
        private readonly string _purchaseUrl;

        public BasketService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _basketByPassUrl = $"http://localhost:5103/api/v1/basket";
            _purchaseUrl = $"http://localhost:5103/api/v1";
        }

        public async Task<Basket> GetBasket(ApplicationUser user)
        {
            var uri = API.Basket.GetBasket(_basketByPassUrl, user.Id);
            var responseString = await _httpClient.GetStringAsync(uri);

            return string.IsNullOrEmpty(responseString) ? new Basket() { BuyerId = user.Id } : responseString.ToObject<Basket>();
        }

        public async Task<Basket> UpdateBasket(Basket basket)
        {
            var uri = API.Basket.UpdateBasket(_basketByPassUrl);

            var basketContent = new StringContent(basket.ToJson(), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, basketContent);
            response.EnsureSuccessStatusCode();

            return basket;
        }

        public async Task Checkout(BasketDTO basket)
        {
            var uri = API.Basket.CheckoutBasket(_basketByPassUrl);
            var basketContent = new StringContent(basket.ToJson(), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, basketContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Basket> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities)
        {
            var uri = API.Purchase.UpdateBasketItem(_purchaseUrl);

            var basketUpdate = new
            {
                BasketId = user.Id,
                Updates = quantities.Select(kvp => new
                {
                    BasketItemId = kvp.Key,
                    NewQty = kvp.Value
                }).ToArray()
            };

            var basketContent = new StringContent(JsonConvert.SerializeObject(basketUpdate), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(uri, basketContent);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return jsonResponse.ToObject<Basket>();
        }

        public async Task<Order> GetOrderDraft(string basketId)
        {
            var uri = API.Purchase.GetOrderDraft(_purchaseUrl, basketId);
            var responseString = await _httpClient.GetStringAsync(uri);
            var response = responseString.ToObject<Order>();
            return response;
        }

        public async Task AddItemToBasket(ApplicationUser user, int productId)
        {
            var uri = API.Purchase.AddItemToBasket(_purchaseUrl);
            var newItem = new
            {
                BuyerId = user.Id,
                Items = new[]
                {
                    new { ProductId = productId, Quantity = 1 }
                }
            };
            var basketContent = new StringContent(newItem.ToJson(), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, basketContent);
        }
    }
}
