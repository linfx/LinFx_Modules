using Dapper;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Models;
using Ordering.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderingContext _context;

        public OrderService(OrderingContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderAsync(int id)
        {
            var connection = _context.Database.GetDbConnection();

            var result = await connection.QueryAsync<dynamic>(
               @"select o.[Id] as ordernumber,o.OrderDate as date, o.Description as description,
                        o.Address_City as city, o.Address_Country as country, o.Address_State as state, o.Address_Street as street, o.Address_ZipCode as zipcode,
                        os.Name as status, 
                        oi.ProductName as productname, oi.Units as units, oi.UnitPrice as unitprice, oi.PictureUrl as pictureurl
                        FROM ordering.Orders o
                        LEFT JOIN ordering.Orderitems oi ON o.Id = oi.orderid 
                        LEFT JOIN ordering.orderstatus os on o.OrderStatusId = os.Id
                        WHERE o.Id=@id"
                    , new { id }
                );

            if (result.AsList().Count == 0)
                throw new KeyNotFoundException();

            return MapOrderItems(result);
        }

        public async Task<IEnumerable<OrderSummary>> GetOrdersFromUserAsync(string userId)
        {
            var connection = _context.Database.GetDbConnection();

            var result = await connection.QueryAsync<OrderSummary>(
                @"SELECT
                     o.Id AS OrderNumber,
                     o.OrderDate AS Date,
                     os.NAME AS Status,
                     SUM( oi.units * oi.unitprice ) AS Total 
                    FROM
                     Orders o
                     LEFT JOIN orderitems oi ON o.Id = oi.orderid
                     LEFT JOIN orderstatus os ON o.OrderStatusId = os.Id
                     LEFT JOIN buyers ob ON o.BuyerId = ob.Id 
                    WHERE
                     ob.Id = @userId
                    GROUP BY
                     o.Id,
                     o.OrderDate,
                     os.NAME 
                    ORDER BY
                     o.Id", new { userId });

            return result;
        }

        public async Task<IEnumerable<CardType>> GetCardTypesAsync()
        {
            var connection = _context.Database.GetDbConnection();

            var result = await connection.QueryAsync<CardType>(
                @"SELECT
	                Id,NAME 
                FROM
	                CardTypes");

            return result;
        }

        private Order MapOrderItems(dynamic result)
        {
            var order = new Order
            {
                OrderNumber = result[0].ordernumber,
                Date = result[0].date,
                Status = result[0].status,
                Description = result[0].description,
                Street = result[0].street,
                City = result[0].city,
                Zipcode = result[0].zipcode,
                Country = result[0].country,
                OrderItems = new List<Orderitem>(),
                Total = 0
            };

            foreach (dynamic item in result)
            {
                var orderitem = new Orderitem
                {
                    ProductName = item.productname,
                    Units = item.units,
                    Unitprice = (double)item.unitprice,
                    Pictureurl = item.pictureurl
                };

                order.Total += item.units * item.unitprice;
                order.OrderItems.Add(orderitem);
            }

            return order;
        }
    }
}
