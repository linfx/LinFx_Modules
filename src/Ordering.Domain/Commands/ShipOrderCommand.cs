using MediatR;
using System.Runtime.Serialization;

namespace Ordering.Domain.Commands
{
    /// <summary>
    /// 订单发货
    /// </summary>
    public class ShipOrderCommand : IRequest<bool>
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [DataMember]
        public int OrderNumber { get; private set; }

        public ShipOrderCommand(int orderNumber)
        {
            OrderNumber = orderNumber;
        }
    }
}