using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace FoodieStore.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [DisplayName("Order Date")]
        public DateTime OrderDate { get; set; }

        [DisplayName("Order State")]
        public string OrderState { get; set; }

        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        public string City { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        public string Email { get; set; }

        public decimal Total { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

    }
}