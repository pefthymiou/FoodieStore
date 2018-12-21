using FoodieStore.Models;
using FoodieStore.Tools;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FoodieStore.Controllers
{
    public class CheckoutController : Controller
    {
        App_Context storeDB = new App_Context();


        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AddressAndPayment(FormCollection values)
        {

            var order = new Order();
            TryUpdateModel(order);
            try
            {
                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;
                order.OrderState = "Pending";
                order.UserId = storeDB.Users.Where(x => x.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().UserId;
                order.Email = storeDB.Users.Where(x => x.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().Email;
                storeDB.Orders.Add(order);
                storeDB.SaveChanges();

                var cart = ShoppingCart.GetCart(this.HttpContext);
                cart.CreateOrder(order);
                EmailHandler email = new EmailHandler();
                await email.OrderEmail(order);
                return RedirectToAction("Complete", "Checkout", new { id = order.OrderId });
            }
            catch (Exception ex)
            {
                return View(order);
            }
        }

        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                storeDB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}