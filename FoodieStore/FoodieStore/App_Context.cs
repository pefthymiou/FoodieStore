namespace FoodieStore
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using FoodieStore.Models;

    public class App_Context : DbContext
    {
        // Your context has been configured to use a 'App_Context' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'FoodieStore.App_Context' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'App_Context' 
        // connection string in the application configuration file.
        public App_Context()
            : base("name=App_Context")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CartVM> Carts { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}