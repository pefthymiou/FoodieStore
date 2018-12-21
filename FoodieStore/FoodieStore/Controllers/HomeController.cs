using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodieStore.Models;
using FoodieStore.Repositories;

namespace FoodieStore.Controllers
{
    
    public class HomeController : Controller
    {
        ProductsRepository productsRepository = new ProductsRepository();
        OrdersRepository ordersRepos = new OrdersRepository();

        [Authorize]
        public ActionResult CustOrders(string name)
        {
            List<OrdersJoin> customerOrders = new List<OrdersJoin>();
            using (App_Context db = new App_Context())
            {
                int userId = db.Users.Where(n => n.Username == name).Select(i => i.UserId).FirstOrDefault();
                //customerOrders = db.Orders.Where(c => c.UserId == userId).OrderByDescending(c => c.OrderDate).ToList();
                customerOrders = (from ord in db.Orders
                                  join ordDet in db.OrderDetails on ord.OrderId equals ordDet.OrderId
                                  join prod in db.Products on ordDet.ProductId equals prod.ProductId
                                  where ord.Username == name
                                  select new OrdersJoin
                                  {
                                      State = ord.OrderState,
                                      OrderDate = ord.OrderDate,
                                      Quantity = ordDet.Quantity,
                                      ProductPrice = ordDet.ProductPrice,
                                      ProductName = prod.ProductName
                                  }).ToList();

                Session["CustomDet"] = userId;

                //if (name != db.Users.)
            }



            return View(customerOrders);
        }

        [Authorize]
        public ActionResult EditAccount(string name)
        {
            User user;
            using (var db = new App_Context())
            {
                user = db.Users.Where(i => i.Username == name).SingleOrDefault();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountSubmit(User user)
        {
            if (!ModelState.IsValid)
            {
                return View("EditAccount",user);
            }
            User _user = new User();
            using(var db = new App_Context())
            {
                _user = db.Users.SingleOrDefault(i => i.Username == user.Username);
                if (_user!=null)
                {
                    _user.Username = user.Username;
                    _user.Password = user.Password;
                    _user.Salt = user.Salt;
                    _user.Email = user.Email;
                    _user.City = user.City;
                    _user.Firstname = user.Firstname;
                    _user.Lastname = user.Lastname;
                    _user.Phone = user.Phone;
                    _user.RoleId = user.RoleId;
                    _user.Address = user.Address;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index","Home");
        }

        List<Product> product = new List<Product>();
        // GET: Home
        public ActionResult Index()
        {
            return View(productsRepository.GetAllDishes());
        }

        public ActionResult DailyDishes()
        {
            Random r = new Random();
            List<Product> allDishes;
            using (var db = new App_Context())
            {
                allDishes = db.Products.Where(i => i.Category.CategoryId == 2).Take(3).ToList();

            }

            return View(allDishes);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Menu()
        {
            
            return View(productsRepository.GetAllProducts());
        }

        public ActionResult ProductByCat(int id)
        {

            using (var db = new App_Context())
            {
                var allProducts = db.Products.Where(i => i.Category.CategoryId == id).ToList();
                product = allProducts;
            }
            return View(product);
        }
    }
}