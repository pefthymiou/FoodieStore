using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodieStore.Models
{
    public class Category
    {
        [Key]
        [Display(Name ="Category Id")]
        public int CategoryId { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}