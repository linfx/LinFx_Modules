using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopFx.Ordering.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    [ApiVersion("v1")]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;
        //private readonly IOrderQueries _orderQueries;
        //private readonly IIdentityService _identityService;

        //public OrdersController(IMediator mediator, IOrderQueries orderQueries, IIdentityService identityService)
        //{

        //    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        //    _orderQueries = orderQueries ?? throw new ArgumentNullException(nameof(orderQueries));
        //    _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        //}
    }
}
