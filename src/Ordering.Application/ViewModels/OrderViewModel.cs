using System;
using System.Collections.Generic;

namespace Ordering.Application.Models
{
    public class Orderitem
    {
        public string ProductName { get; set; }
        public int Units { get; set; }
        public double Unitprice { get; set; }
        public string Pictureurl { get; set; }
    }

    public class Order
    {
        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public List<Orderitem> OrderItems { get; set; }
        public decimal Total { get; set; }
    }

    public class OrderSummary
    {
        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public double Total { get; set; }
    }

    public class CardType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
