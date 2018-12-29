using System;
using System.Collections.Generic;
using System.Linq;

namespace WebMvc.ViewModels
{
    public class Basket
    {
        public string BuyerId { get; set; }

        // Use property initializer syntax.
        // While this is often more useful for read only 
        // auto implemented properties, it can simplify logic
        // for read/write properties.
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public decimal Total()
        {
            return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity),2);
        }
    }
}
