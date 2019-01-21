using LinFx.Domain.Models;

namespace Payment.Domain.Models.OrderAggregate
{
    /// <summary>
    /// 订单订单
    /// </summary>
    public class Order : AggregateRoot<long>
    {
        // DDD Patterns comment
        // Using private fields, allowed since EF Core 1.1, is a much better encapsulation
        // aligned with DDD Aggregates and Domain Entities (Instead of properties and property collections)
        private int _mchId;            //商户ID
        private string _outTradeNo;    //商户订单号
        private string _currency;      //三位货币代码, 人民币(cny)
        private int _totalAmount;      //支付金额, 单位(分)
        private int _orderStatusId;
        private string _subject;
        private string _body;

        // Value Object pattern example persisted as EF Core 2.0 owned entity
        public OrderStatus OrderStatus { get; private set; }

        public void SetPaidStatus()
        {
            if (_orderStatusId == OrderStatus.Awaiting.Id)
            {
                //AddDomainEvent(new OrderStatusChangedToPaidDomainEvent(Id, OrderItems));
                _orderStatusId = OrderStatus.Paid.Id;
                //_description = "The payment was performed at a simulated \"American Bank checking bank account endinf on XX35071\"";
            }
        }
    }
}
