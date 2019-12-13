using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using internet_shop.Models;
using internet_shop.DbContexts;

namespace internet_shop.Services
{
    public class BrandService
    {
        public BrandService(BrandDbContext db, ProductDbContext dbProduct)
        {
            _db = db;
            _dbProduct = dbProduct;

        }

        private readonly BrandDbContext _db;
        private readonly ProductDbContext _dbProduct;
        private DbSet<Brand> Brand => _db.Brands;
        private DbSet<Product> Products => _dbProduct.Products;

        public List<Brand> GetAllBrand()
        {
            return Brand.ToList();
        }

        public List<Product> GetProductsByBrand(int id)
        {
            var products = Products.Where((x) => x.BrandId == id).ToList();
            if (products == null || products.Count == 0)
                return null;
            else
                return products;
        }

        public Brand GetBrandById(int id)
        {
            var brand = Brand.SingleOrDefault((Brand brand) => brand.Id == id);
            if (brand == null)
            {
                return null;
            }
            return brand;
        }

        public bool DeleteBrandById(int id)
        {
            Brand brand = Brand.SingleOrDefault((Brand brand) => brand.Id == id);

            if (brand == null)
            {
                return false;
            }
            try
            {
                _db.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool AddBrand(string name)
        {
            Brand brand = ToEntity(name);
            Brand.Add(brand);
            try
            {
                _db.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public Brand ToEntity(string name)
        {
            return new Brand
            {
                Name = name
            };
        }
    }
}
