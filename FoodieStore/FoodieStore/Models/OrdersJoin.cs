using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodieStore.Models
{
    public class OrdersJoin
    {
        public string State { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? Quantity { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ProductName { get; set; }
    }
}