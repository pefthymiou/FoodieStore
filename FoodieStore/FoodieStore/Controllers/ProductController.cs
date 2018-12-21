using FoodieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodieStore.Repositories;

namespace FoodieStore.Controllers
{
    public class ProductController : Controller
    {
        ProductsRepository productsRepository = new ProductsRepository();

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Product(string searchString)
        {
            var db = new App_Context();
            List<Product> products;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = productsRepository.productSearch(searchString);      
            }
            else
            {
                products = productsRepository.GetAllProducts();
            }
            return View(products);
        }

        //Παίρνει το νέο προίον και το βάζει στη βάση
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubmitProduct(Product productNew)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", productNew);
            }
            Product product = new Product();
            Category category = new Category();
            using (App_Context db = new App_Context())
            {
                category = db.Categories.Find(productNew.CategoryId);
                product.ProductName = productNew.ProductName;
                product.ProductPrice = productNew.ProductPrice;
                product.Category = category;
                product = productNew;
                db.Products.Add(productNew);
                db.Entry(category).State = System.Data.Entity.EntityState.Unchanged;
                db.SaveChanges();
            }
            return RedirectToAction("Product", "Product");
        }

        // GET: Product
        public ActionResult Edit(int id)
        {
            return View(productsRepository.productDetails(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Μην ξεχάσεις @Html.AntiForgeryToken() στο view
        public ActionResult EditSubmitProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", product);
            }
            Product dProduct = new Product();
            using (App_Context db = new App_Context())
            {
                dProduct = db.Products.Include("Category").SingleOrDefault(d => d.ProductId == product.ProductId);
                
                if (dProduct == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    dProduct.ProductName = product.ProductName;
                    dProduct.ProductPrice = product.ProductPrice;
                    dProduct.ImagePath = product.ImagePath;
                    dProduct.CategoryId = product.CategoryId;
                    
                    //ΠΡΟΣΟΧΗ Σκάει
                    db.Entry(dProduct.Category).State = System.Data.Entity.EntityState.Unchanged;
                    db.SaveChanges();   
                }
                return RedirectToAction("Product", "Product");
            }
        }

        public ActionResult DeleteProduct(int id)
        {
            Product product = productsRepository.productDetails(id); 
            return View("DeleteProduct",product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Μην ξεχάσεις @Ηtml.ValidateAntiForgeryToken() στο view
        public ActionResult DeleteProductSubmit(Product product)
        {
            productsRepository.DeleteProduct(product);
            return RedirectToAction("Product","Product");
        }

        public ActionResult Details(int id)
        {
            return View(productsRepository.productDetails(id));
        }
    }
}