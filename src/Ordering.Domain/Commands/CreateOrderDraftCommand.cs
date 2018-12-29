using MediatR;
using Ordering.Domain.Models;
using System.Collections.Generic;

namespace Ordering.Domain.Commands
{
    /// <summary>
    /// 创建订单预览
    /// </summary>
    public class CreateOrderDraftCommand :  IRequest<OrderDraftDTO>
    {
        public string BuyerId { get; private set; }

        public IEnumerable<BasketItem> Items { get; private set; }

        public CreateOrderDraftCommand(string buyerId, IEnumerable<BasketItem> items)
        {
            BuyerId = buyerId;
            Items = items;
        }
    }

}
