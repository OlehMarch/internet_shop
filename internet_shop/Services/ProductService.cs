using System.Linq;
using System.Collections.Generic;

using internet_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace internet_shop.Services
{
    public class ProductService
    {
        public ProductService(BaseDbContext db)
        {
            _db = db;
        }

        private readonly BaseDbContext _db;
        private DbSet<Product> Products => _db.Products;

        public bool AddNewProduct(string name, string description, int brandId, int categoryId, int price, int updatedPrice)
        {
            var product = ToEntity(name, description, brandId, categoryId, price, updatedPrice);
            if (product == null)
                return false;
            else
            {
                Products.Add(product);
                _db.SaveChanges();
                product = ProductToDTO(product);
                Products.Update(product);
                _db.SaveChanges();
                return true;
            }
        }
        public List<Product> GetAll()
        {
            return Products.ToList();
        }

        public bool UpdateProduct()
        {
            var products = Products.ToList();
            for (int i = 0; i < products.Count; i++)
            {
                products[i] = ProductToDTO(products[i]);
                Products.Update(products[i]);
            }
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Remove(int id)
        {
            using (BaseDbContext db = new BaseDbContext()) // TODO(friday13): change to DI usage
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
            using (BaseDbContext db = new BaseDbContext()) // TODO(friday13): change to DI usage
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

        public Product ToEntity(string name, string description, int brandId, int categoryId, int price, int updatedPrice)
        {
            return new Product
            {
                Name = name,
                Description = description,
                BrandId = brandId,
                CategoryId = categoryId,
                Price = price,
                UpdatedPrice = updatedPrice,
            };
        }

        public Product ProductToDTO(Product product)
        {
            var newPrice = EntryPromos(product.Id);
            product.UpdatedPrice = product.Price - ((product.Price * newPrice) / 100);
            return product;
        }

        public int EntryPromos(int id)
        {
            var product = Products.SingleOrDefault((Product product) => product.Id == id);
            //List<Promos> promos = new List<Promos>();
            var promos = _db.Promos.Where((x) => x.IsEnabled == true).ToList();

            //var promos =(_db.Promos.GroupBy((x)=> x.IsEnabled == true).ToList());

            return ForToPromos(promos, product);
        }

        public int ForToPromos(List<Promos> promos, Product product)
        {
            List<Promos> promoList = new List<Promos>();
            for (int item = 0; item < promos.Count; item++)
            {
                if (product.CategoryId == promos[item].CategoryId ||
                    product.BrandId == promos[item].BrandId ||
                    product.Id == promos[item].ProductId)
                {
                    promoList.Add(promos[item]);
                }
            }
            if (promoList.Count == 0)
            {
                return 0;
            }
            else
            {
                int promo = promoList.Max(x => x.Value);
                return promo;
            }
        }
    }
}
