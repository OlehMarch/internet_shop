using Internet_shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_shop.Services
{
    public class ProductService
    {
        DbSet<Product> Product;
        public bool AddNewProduct(string name, string description, int price)
        {
            using (ProductContext db = new ProductContext())
            {
                var product = ToEntity(name, description, price);
                if (product == null)
                    return false;
                else
                {
                    db.Product.Add(product);
                    db.SaveChanges();
                    return true;
                }
            }
        }
        public List<Product> GetAll()
        {
            using (ProductContext db = new ProductContext())
            {
                return db.Product.ToList();
            }
        }

        public bool Remove(int id)
        {
            using (ProductContext db = new ProductContext())
            {
                var products = db.Product.Find(id);
                if(products == null)
                    return false;
                else
                {
                    db.Product.Remove(products);
                    db.SaveChanges();
                    return true;
                }
                
            }
        }
        public Product GetProduct(int id)
        {
            using (ProductContext db = new ProductContext())
            {
                if(db.Product.Find(id) == null)
                {
                    return null;
                }
                else
                {
                    return db.Product.Find(id);
                }
            }
        }

        public Product ToEntity(string name, string description, int price)
        {
            return new Product { Name = name, Description = description, Price = price };
        }
    }
}
