using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodieStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string ProductName { get; set; }

        public string Description { get; set; }
        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }
        [Display(Name ="Image Url")]
        [StringLength(1024)]
        public string ImagePath { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}