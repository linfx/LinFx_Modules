using LinFx.Extensions.EventBus.Abstractions;
using LinFx.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Basket.Api.Models;
using Basket.Api.Services;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using Basket.API.Application.IntegrationEvents.Events;
using Microsoft.AspNetCore.Authorization;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/basket")]
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

        [HttpPut]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody]CustomerBasket value)
        {
            var basket = await _repository.UpdateBasketAsync(value);
            return Ok(basket);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]CustomerBasket value)
        {
            var basket = await _repository.GetBasketAsync(value.BuyerId);
            if (basket == null)
            {
                basket = new CustomerBasket(value.BuyerId);
            }

            foreach (var item in value.Items)
            {
                var tmp = basket.Items.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (tmp == null)
                {
                    basket.Items.Add(item);
                }
                else
                {
                    tmp.Quantity++;
                }
            }

            await _repository.UpdateBasketAsync(basket);
            return Ok(basket);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _repository.DeleteBasketAsync(id);
        }

        /// <summary>
        /// 订单结算
        /// </summary>
        /// <param name="basketCheckout"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [Authorize]
        [Route("checkout")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody]BasketCheckout basketCheckout, [FromHeader(Name = "x-requestid")] string requestId)
        {
            var userId = _identitySvc.GetUserIdentity();

            basketCheckout.RequestId = (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty) ?
                guid : basketCheckout.RequestId;

            var basket = await _repository.GetBasketAsync(userId);

            if (basket == null)
                return BadRequest();

            var userName = User.Identity.Name;

            var eventMessage = new UserCheckoutAcceptedIntegrationEvent(userId, userName, basketCheckout.City, basketCheckout.Street,
                basketCheckout.State, basketCheckout.Country, basketCheckout.ZipCode, basketCheckout.CardNumber, basketCheckout.CardHolderName,
                basketCheckout.CardExpiration, basketCheckout.CardSecurityNumber, basketCheckout.CardTypeId, basketCheckout.Buyer, basketCheckout.RequestId, basket);

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process
            await _eventBus.PublishAsync(eventMessage);

            return Accepted();
        }
    }
}