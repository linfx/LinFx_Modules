using LinFx.Security.Principal;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Models;
using Ordering.Application.Services;
using Ordering.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/orders")]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IOrderService _orderService;
        private readonly IHttpContextPrincipalAccessor _httpContextPrincipalAccessor;

        public OrdersController(
            IMediator mediator,
            IOrderService orderService,
            IHttpContextPrincipalAccessor httpContextPrincipalAccessor)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _httpContextPrincipalAccessor = httpContextPrincipalAccessor ?? throw new ArgumentNullException(nameof(httpContextPrincipalAccessor));
        }

        /// <summary>
        /// 订单取消
        /// </summary>
        /// <param name="command"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [Route("cancel")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelOrder([FromBody]CancelOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<CancelOrderCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestCancelOrder);
            }
            return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();

        }

        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name = "command" ></ param >
        /// < param name="requestId"></param>
        /// <returns></returns>
        [Route("ship")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ShipOrder([FromBody]ShipOrderCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestShipOrder = new IdentifiedCommand<ShipOrderCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestShipOrder);
            }
            return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [Route("{orderId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(orderId);
                return Ok(order);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderSummary>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrders()
        {
            var userid = _httpContextPrincipalAccessor.Principal.FindUserId();
            var orders = await _orderService.GetOrdersFromUserAsync(userid);
            return Ok(orders);
        }

        [Route("cardTypes")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CardType>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCardTypes()
        {
            var cardTypes = await _orderService.GetCardTypesAsync();
            return Ok(cardTypes);
        }

        /// <summary>
        /// 订单预览
        /// </summary>
        /// <param name="createOrderDraftCommand"></param>
        /// <returns></returns>
        [Route("draft")]
        [HttpPost]
        public async Task<IActionResult> GetOrderDraftFromBasketData([FromBody]CreateOrderDraftCommand createOrderDraftCommand)
        {
            var draft = await _mediator.Send(createOrderDraftCommand);
            return Ok(draft);
        }
    }
}
