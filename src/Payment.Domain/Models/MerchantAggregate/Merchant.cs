using LinFx.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment.Domain.Models.MerchantAggregate
{
    /// <summary>
    /// 商户
    /// </summary>
    public class Merchant : AggregateRoot
    {
        private List<PaymentChannel> _paymentChannels;

        public Merchant()
        {
            _paymentChannels = new List<PaymentChannel>();
        }

        public string No { get; set; }
        public string Name { get; set; }
    }
}
