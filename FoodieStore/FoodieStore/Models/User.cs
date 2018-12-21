using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FoodieStore.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Salt { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        [EmailAddress(ErrorMessage = "Λάθος μορφή email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        public string City { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Το πεδίο είναι απαραίτητο")]
        [Phone(ErrorMessage = "Λάθος μορφή τηλεφώνου")]
        public string Phone { get; set; }

        //To vazoume apo panw gia na dhlwsoume oti prepei na psaksei ena pedio RoleId ston pinaka to opoio deixnei sto Role
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}