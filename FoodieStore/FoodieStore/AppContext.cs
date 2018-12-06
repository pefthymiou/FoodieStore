using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using FoodieStore.Models;

namespace FoodieStore
{
    public class AppContext : DbContext
    {
        public AppContext() : base("name=AppContext")
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(i => i.UserId)
                .HasMany<Order>(o => o.Orders);

            modelBuilder.Entity<Order>()
                .ToTable("Orders")
                .HasKey(i => i.OrderId);

            modelBuilder.Entity<Product>()
                .ToTable("Products")
                .HasKey(i => i.ProductId);

            modelBuilder.Entity<Role>()
                .ToTable("Roles")
                .HasKey(i => i.RoleId)
                .HasMany<User>(u => u.Users);

            modelBuilder.Entity<Category>()
                .ToTable("Categories")
                .HasKey(i => i.CategoryId)
                .HasMany<Product>(p => p.Products);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

        }

    }
}