using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FoodieStore.Models;

namespace FoodieStore.Constraints
{
    public class UniqueEmail:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;

            using (App_Context db = new App_Context())
            {
                List<User> allUsers = db.Users.ToList();
                foreach (User u in allUsers)
                {
                    if (u.Email == user.Email)
                        return new ValidationResult("Το Email υπάρχει ήδη");
                }
                return ValidationResult.Success;
            }
        }
    }
}