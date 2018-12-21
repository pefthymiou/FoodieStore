using FoodieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodieStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
        public ActionResult Dashboard()
        {
            List<User> users = new List<User>();
            using (App_Context db = new App_Context())
            {
                users = db.Users.ToList();
            }
            return View(users);
        }

        public ActionResult ReturnUsers()
        {
            List<User> users = new List<User>();
            using (App_Context db = new App_Context())
            {
                users = db.Users.ToList();
            }
            //var vm = new GlobalModel();
            return View(users);
        }

        //Zητάει το id του χρήστη και φέρνει πίσω όλα τα στοιχεία του λεπτομερώς
        //Πιθανή χρήση σε button "Details" κάτω από κάθε χρήστη
        public ActionResult UserDetails(int id)
        {
            User detailedUser = new User();
            using (App_Context db = new App_Context())
            {
                detailedUser = db.Users.Include("Role").SingleOrDefault(u => u.UserId == id);
            }
            if (detailedUser == null)
                return HttpNotFound();
            return View(detailedUser);
        }

        //Ζητάει το id του χρήστη και τις νέες τιμές και καταχωρεί τις αλλαγές στη βάση
        public ActionResult EditUser(int id)
        {
            User editedUser = new User();
            using (App_Context db = new App_Context())
            {
                editedUser = db.Users.SingleOrDefault(u => u.UserId == id);
            }
            if (editedUser == null)
                return HttpNotFound();
            return View(editedUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult EditSubmitUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return View("EditUser", user);
            }
            User dUser = new User();
            using (App_Context db = new App_Context())
            {
                dUser = db.Users.SingleOrDefault(d => d.UserId == user.UserId);
                if (dUser == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    dUser.Password = user.Password;
                    dUser.Firstname = user.Firstname;
                    dUser.Lastname = user.Lastname;
                    dUser.Address = user.Address;
                    dUser.PostalCode = user.PostalCode;
                    dUser.City = user.City;
                    dUser.Email = user.Email;
                    dUser.Phone = user.Phone;
                    dUser.Salt = user.Salt;
                    db.SaveChanges();
                }
                return RedirectToAction("Dashboard", "Admin");
            }
        }

        //Λιστα με όλους τους μπαναρισμένους users
        public ActionResult BannedUsers()
        {
            using (App_Context db = new App_Context())
            {
                List<User> allBannedUsers = db.Users.Where(b => b.Role.RoleId == 3).ToList();
                return View(allBannedUsers);
            }
        }

        //Ban τον user με το id...
        public ActionResult Ban(int id)
        {
            User bannedUser = new User();
            Role bannedRole = new Role();
            using (App_Context db = new App_Context())
            {
                bannedUser = db.Users.Include("Role").SingleOrDefault(b => b.UserId == id);
                if (bannedUser == null)
                    return HttpNotFound();
                bannedRole = db.Roles.Find(3);
                bannedUser.Role = bannedRole;
                db.SaveChanges();
            }
            return RedirectToAction("BannedUsers", "Admin");
        }

        public ActionResult LiftBan(int id)
        {
            User unBannedUser = new User();
            Role unBannedRole = new Role();
            using (App_Context db = new App_Context())
            {
                unBannedUser = db.Users.Include("Role").SingleOrDefault(u => u.UserId == id);
                if (unBannedUser == null)
                    return HttpNotFound();
                unBannedRole = db.Roles.Find(2);
                unBannedUser.Role = unBannedRole;
                db.Entry(unBannedRole).State = System.Data.Entity.EntityState.Unchanged;
                db.SaveChanges();
            }
            return RedirectToAction("Dashboard","Admin");
        }

        //Eπιστρέφει όλες τις κατηγορίες σε λίστα
        public ActionResult Categories()
        {
            List<Category> allCategories = new List<Category>();
            using (App_Context db = new App_Context())
            {
                allCategories = db.Categories.ToList();
            }
            return View(allCategories);
        }

        public ActionResult ChangeRole(int id)
        {
            User assignedUser = new User();
            Role assignedRole = new Role();
            using (App_Context db = new App_Context())
            {
                assignedUser = db.Users.Include("Role").SingleOrDefault(b => b.UserId == id);
                if (assignedUser == null)
                    return HttpNotFound();
                if (assignedUser.Role.Rolename == "Admin")
                {
                    assignedRole = db.Roles.Find(2);
                }
                if (assignedUser.Role.Rolename == "User")
                {
                    assignedRole = db.Roles.Find(1);
                }
                assignedUser.Role = assignedRole;
                db.SaveChanges();
            }
            return RedirectToAction("Dashboard", "Admin");
        }
    }
}