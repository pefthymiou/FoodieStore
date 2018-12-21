using FoodieStore.Models;
using FoodieStore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodieStore.Controllers
{
    public class OrderController : Controller
    {

        public ActionResult Order()
        {
            List<Order> orders = new List<Order>();
            using (var db = new App_Context())
            {
                orders = db.Orders.ToList();
            }
            return View(orders);
        }

        public ActionResult OrderProducts(int id)
        {
            List<OrderDetail> details = new List<OrderDetail>();

            using (App_Context db = new App_Context())
            {
                Order order = db.Orders.Include("OrderDetails").SingleOrDefault(d => d.OrderId == id);
                if (order == null)
                    return HttpNotFound();
                details = order.OrderDetails;
                foreach (var item in details)
                {
                    Product product = db.Products.SingleOrDefault(i => i.ProductId == item.ProductId);
                    item.Product.ProductName = product.ProductName;
                }
            }
            return View(details);
        }

        public ActionResult Details(int id)
        {
            Order order = new Order();
            using (var db = new App_Context())
            {
                var totalOrders = db.Orders.Include("OrderDetails").ToList();
                foreach (var item in totalOrders)
                {
                    if (item.OrderId == id)
                    {
                        order = item;
                    }
                }
            }
            return View(order);
        }

        //Zητάει το id της παραγγελίας και φέρνει πίσω όλα τα στοιχεία της λεπτομερώς
        //ενώ ταυτόχρονα την θέτει ως διαβασμένη στη βάση! (state=read)
        public ActionResult ReadOrderChange(int id)
        {
            Order readOrder = new Order();
            using (App_Context db = new App_Context())
            {
                readOrder = db.Orders.SingleOrDefault(r => r.OrderId == id);
                if (readOrder == null)
                {
                    return HttpNotFound();
                }
                if (readOrder.OrderState == "Pending")
                {
                    readOrder.OrderState = "Read";
                    db.SaveChanges();
                }
            }
            return View(readOrder);
        }


        public async System.Threading.Tasks.Task<ActionResult> ChangeState(int id)
        {
            Order order = new Order();
            using (App_Context db = new App_Context())
            {
                order = db.Orders.SingleOrDefault(s => s.OrderId == id);
                if (order == null || order.OrderState != "Pending" && order.OrderState != "Read" && order.OrderState != "Completed")
                {
                    return HttpNotFound();
                }
                if (order.OrderState == "Completed")
                {
                    order.OrderState = "Read";
                }
                else
                {
                    order.OrderState = "Completed";
                    EmailHandler e = new EmailHandler();
                    await e.CompletedEmail(order);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Order", "Order");
        }
    }
}