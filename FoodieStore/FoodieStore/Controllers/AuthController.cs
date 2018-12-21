using FoodieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FoodieStore.Repositories;
using FoodieStore.Tools;
using System.Threading.Tasks;

namespace FoodieStore.Controllers
{
    public class AuthController : Controller
    {
        private readonly EmailHandler emailHandler = new EmailHandler();
        private readonly string passPattern = @"^(?=(.*[a-zA-Z].*){2,})(?=.*\d.*)(?=.*\W.*)[a-zA-Z0-9\S]{5,15}$";
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(User user)
        {

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            bool admincheck = false;
            

            using (App_Context db = new App_Context())
            {
                if (db.Users.ToList().Count == 0)
                {
                    admincheck = true;
                }

                var existUser = db.Users.Where(i => i.Username == user.Username).ToList();
                var count = existUser.Count;
                if(count>0)
                {
                    ViewData["UserExist"] = existUser;
                    return View(user);
                }

                bool isValidPass = Regex.IsMatch(user.Password, passPattern);
                if (!isValidPass && user.Username != "god")
                {
                    ViewData["InvalidPass"] = existUser;
                    return View(user);
                }

                if (count == 0)
                {

                    var salt = Password.GetSalt();
                    var hash = Password.Hash(user.Password, salt);
                    Role r = new Role();
                    if (admincheck == true)
                    {
                        r = db.Roles.Find(1);
                    }
                    else
                    {
                        r = db.Roles.Find(2);
                    }
                    
                    User u = new User
                    {
                        Username = user.Username,
                        Password = Convert.ToBase64String(hash),
                        Salt = Convert.ToBase64String(salt),
                        Firstname = user.Firstname,
                        Lastname = user.Lastname,
                        Phone = user.Phone,
                        Address = user.Address,
                        City = user.City,
                        Email = user.Email,
                        PostalCode = user.PostalCode,
                        Role = r
                    };
                    db.Users.Add(u);
                    db.Entry(r).State = System.Data.Entity.EntityState.Unchanged;
                    db.SaveChanges();
                    ViewData["Success"] = existUser;
                    await emailHandler.RegistrationEmail(user.Email, user.Firstname, user.Username, user.Password);
                }
            }
            return View();
        }


        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM login)
        {
            User user = new User();
            string dbpass;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            using (App_Context db = new App_Context())
            {
                // επιστέφω μόνο το salt του χρήστη που κάνει Login
                string salt = db.Users.Where(i => i.Username == login.Username).Select(p => p.Salt).SingleOrDefault();
                if (salt == null)
                {
                    // ViewData["IncorrectUsername"] = user;
                    return RedirectToAction("Index", "Home");
                }

                byte[] hash = Password.Hash(login.Password, Convert.FromBase64String(salt));

                dbpass = Convert.ToBase64String(hash);

                user = db.Users.Include("Role")
                    .SingleOrDefault(i => i.Username == login.Username && i.Password == dbpass);
            }

            if (user != null)
            {
                
                if (user.Role.Rolename == "Banned")
                {
                    return Logout();
                }
                else
                {
                    Role userRoles = user.Role;
                    //Session["UserName"] = user.Username;
                    var ticket = new FormsAuthenticationTicket(version: 1,
                                       name: login.Username,
                                       issueDate: DateTime.Now,
                                       expiration: DateTime.Now.AddDays(5),
                                       isPersistent: login.RememberMe,
                                       userData: userRoles.Rolename);

                    var encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                        encryptedTicket);
                    HttpContext.Response.Cookies.Add(cookie);

                    if (user.Role.Rolename == "Admin")
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    if (login.Username == "god" && login.Password == "god")
                    {
                        return RedirectToAction("Order", "Order");
                    }
                }
            }
            else
            {
                ViewData["NullUser"] = login;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}