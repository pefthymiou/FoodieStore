using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodieStore.Models
{
    public class ShoppingCartVM
    {
        public List<CartVM> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}