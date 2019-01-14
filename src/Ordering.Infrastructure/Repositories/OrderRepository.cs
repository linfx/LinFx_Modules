using LinFx.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Interfaces;
using Ordering.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderingContext _context;

        public OrderRepository(OrderingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork { get { return _context; } }

        public Order Add(Order order)
        {
            return _context.Orders.Add(order).Entity;
        }

        public void Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }

        public async Task<Order> GetAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                await _context.Entry(order).Collection(i => i.OrderItems).LoadAsync();
                await _context.Entry(order).Reference(i => i.OrderStatus).LoadAsync();
                await _context.Entry(order).Reference(i => i.Address).LoadAsync();
            }
            return order;
        }
    }
}
