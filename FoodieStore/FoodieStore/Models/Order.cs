using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FoodieStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderState { get; set; }

        public User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}