using FoodieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodieStore.Repositories
{
    public class UsersRepository
    {
        public List<User> GetAllUsers()
        {
            List<User> allUsers;
            using (App_Context db = new App_Context())
            {
                allUsers = db.Users.Include("Role").ToList();
            }
            return allUsers;
        }

        public User GetUserById(int id)
        {
            User user;
            using (App_Context db = new App_Context())
            {
                user = db.Users.Include("Role").Include("Order").FirstOrDefault(u => u.UserId == id);
            }
            return user;
        }

        public List<User> GetBannedUsers()
        {
            List<User> bannedUsers;
            using (App_Context db = new App_Context())
            {
                bannedUsers = db.Users.Where(u => u.RoleId == 3).ToList();
            }
            return bannedUsers;
        }
    }
}