using System.Linq;
using System.Collections.Generic;

using internet_shop.Models;
using internet_shop.DbContexts;

namespace internet_shop.Services
{
    public class ProductService
    {
        public bool AddNewProduct(string name, string description, int price)
        {
            using (ProductDbContext db = new ProductDbContext())
            {
                var product = ToEntity(name, description, price);
                if (product == null)
                    return false;
                else
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return true;
                }
            }
        }
        public List<Product> GetAll()
        {
            using (ProductDbContext db = new ProductDbContext())
            {
                return db.Products.ToList();
            }
        }

        public bool Remove(int id)
        {
            using (ProductDbContext db = new ProductDbContext())
            {
                var products = db.Products.Find(id);
                if (products == null)
                    return false;
                else
                {
                    db.Products.Remove(products);
                    db.SaveChanges();
                    return true;
                }

            }
        }
        
        public Product GetProduct(int id)
        {
            using (ProductDbContext db = new ProductDbContext())
            {
                if (db.Products.Find(id) == null)
                {
                    return null;
                }
                else
                {
                    return db.Products.Find(id);
                }
            }
        }

        public Product ToEntity(string name, string description, int price)
        {
            return new Product { Name = name, Description = description, Price = price };
        }
    }
}
