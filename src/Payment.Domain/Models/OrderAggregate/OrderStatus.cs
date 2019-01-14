using LinFx.Domain.Models;
using Ordering.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Payment.Domain.Models.OrderAggregate
{
    public class OrderStatus : Enumeration
    {
        /// <summary>
        /// 订单提交
        /// </summary>
        public static OrderStatus Submitted = new OrderStatus(1, nameof(Submitted).ToLowerInvariant());
        /// <summary>
        /// 等待支付
        /// </summary>
        public static OrderStatus Awaiting = new OrderStatus(2, nameof(Awaiting).ToLowerInvariant());
        /// <summary>
        /// 支付成功
        /// </summary>
        public static OrderStatus Paid = new OrderStatus(3, nameof(Paid).ToLowerInvariant());
        /// <summary>
        /// 取消支付
        /// </summary>
        public static OrderStatus Cancelled = new OrderStatus(5, nameof(Cancelled).ToLowerInvariant());
        /// <summary>
        /// 业务已完成
        /// </summary>
        public static OrderStatus Completed = new OrderStatus(6, nameof(Completed).ToLowerInvariant());

        protected OrderStatus() { }

        public OrderStatus(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<OrderStatus> List() => new[] { Submitted, Awaiting, Completed, Paid, Cancelled };

        public static OrderStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
                throw new LinFxDomainException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static OrderStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
                throw new LinFxDomainException($"Possible values for OrderStatus: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
