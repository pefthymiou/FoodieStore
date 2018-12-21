using FoodieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodieStore.Repositories
{
    public class OrdersRepository
    {
        //Φέρνει όλες τις καταχωρημένες παραγγελίες στο σύστημα
        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            using (App_Context db = new App_Context())
            {
                orders = db.Orders.ToList();
            }
            return orders;
        }

        //Φέρνει μια συγκεκριμένη παραγγελία από το id της
        public Order GetOrderById(int id)
        {
            Order order = new Order();
            using (App_Context db = new App_Context())
            {
                
                order = db.Orders.SingleOrDefault(o => o.OrderId == id);
            }
            return order;
        }

        //Φέρνει όλες τις παραγγελίες από το id του πελάτη
        //(άπό την νεότερη στην παλαιότερη)
        //public List<Order> GetCustomerOrders(string name)
        //{
        //    List<Order> customerOrders = new List<Order>();
        //    using (App_Context db = new App_Context())
        //    {
        //        int userId = db.Users.Where(n => n.Username == name).Select(i => i.UserId).FirstOrDefault();
        //        customerOrders = db.Orders.Where(c => c.UserId == userId).OrderByDescending(c => c.OrderDate).ToList();
        //    }
        //    return customerOrders;
        //}

    }
}