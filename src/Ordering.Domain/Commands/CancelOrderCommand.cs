using MediatR;
using System.Runtime.Serialization;

namespace Ordering.Domain.Commands
{
    /// <summary>
    /// 取消订单
    /// </summary>
    public class CancelOrderCommand : IRequest<bool>
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMember]
        public int OrderNumber { get; private set; }

        public CancelOrderCommand(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}
