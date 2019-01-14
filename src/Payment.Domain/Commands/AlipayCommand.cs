using MediatR;

namespace Payment.Domain.Commands
{
    public class AlipayCommand : IRequest<bool>
    {
        public string OutTradeNo { get; private set; }
        public int TotalAmount { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }

        public AlipayCommand(string outTradeNo, int totalAmount, string subject, string body)
        {
            OutTradeNo = outTradeNo;
            TotalAmount = totalAmount;
            Subject = subject;
            Body = body;
        }
    }
}
