using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodieStore.Models
{
    public class Role
    {
        public int RoleId { get; set; }
<<<<<<< HEAD
        public string Rolename { get; set; }

        public virtual ICollection<User> Users { get; set; }
=======
        public string RoleName { get; set; }
>>>>>>> master
    }
}