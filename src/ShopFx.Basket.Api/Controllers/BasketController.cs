using LinFx.Extensions.EventBus.Abstractions;
using Microsoft.AspNetCore.Mvc;
using ShopFx.Basket.Api.Models;
using ShopFx.Services;
using System.Net;
using System.Threading.Tasks;

namespace ShopFx.Basket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IIdentityService _identitySvc;
        private readonly IEventBus _eventBus;

        public BasketController(IBasketRepository repository,
            IIdentityService identityService,
            IEventBus eventBus)
        {
            _repository = repository;
            _identitySvc = identityService;
            _eventBus = eventBus;
        }

        // GET /id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string id)
        {
            var basket = await _repository.GetBasketAsync(id);
            if (basket == null)
            {
                return Ok(new CustomerBasket(id) { });
            }
            return Ok(basket);
        }
    }
}