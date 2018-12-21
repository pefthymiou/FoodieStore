using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FoodieStore.Models;

namespace FoodieStore.Repositories
{
    public class ProductsRepository
    {
        public List<Product> GetAllProducts()
        {
            List<Product> allProducts;
            using (App_Context db = new App_Context())
            {
                allProducts = db.Products.Include("Category").ToList();
            }
            return allProducts;
        }

        public List<Product> GetAllDishes()
        {
            List<Product> allDishes;
            using (App_Context db = new App_Context())
            {
                allDishes = db.Products.Where(i => i.CategoryId == 2).ToList();

            }
            return allDishes;
        }

        public Product productDetails(int id)
        {
            Product product;
            using (App_Context db = new App_Context())
            {
                product = db.Products.Include("Category").SingleOrDefault(i => i.ProductId == id);
            }
            return product;
        }

        public List<Product> productSearch(string searchString)
        {
            List<Product> products;
            using (App_Context db = new App_Context())
            {
                products = db.Products.Include("Category").Where(i => i.ProductName.Contains(searchString)).ToList();
            }
            return products;
        }

        public void DeleteProduct(Product product)
        {
            using (App_Context db = new App_Context())
            {
                Product deletedProduct = db.Products.Find(product.ProductId);
                db.Products.Remove(deletedProduct);
                db.SaveChanges();
            }
        }
    }
}