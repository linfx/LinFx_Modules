using MediatR;
using Payment.Domain.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Domain.CommandHandlers
{
    public class AlipayCommandHandler : IRequestHandler<AlipayCommand, bool>
    {
        public Task<bool> Handle(AlipayCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
