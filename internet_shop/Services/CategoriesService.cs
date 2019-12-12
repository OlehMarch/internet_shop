using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using internet_shop.Models;
using internet_shop.DbContexts;

namespace internet_shop.Services
{
    public class CategoriesService
    {

        public CategoriesService(CategoryDbContext db, ProductDbContext dbProduct)
        {
            _db = db;
            _dbProduct = dbProduct;
        }
        private readonly ProductDbContext _dbProduct;
        private readonly CategoryDbContext _db;
        private DbSet<Product> _Products => _dbProduct.Products;
        private DbSet<Categories> _Categories => _db.Categories;

        public List<Categories> GetAllCategory()
        {
            return _Categories.ToList();
        }

        public List<Product> GetProductsByCategory(int id)
        {
            var products = _Products.Where((x)=> x.CategoryId == id).ToList();
            if (products == null || products.Count == 0)
                return null;
            else
                return products;
        }

        public Categories GetCategoryById(int id)
        {
            var cat = _Categories.SingleOrDefault((Categories cat) => cat.Id == id);
            if (cat == null)
            {
                return null;
            }
            return cat;
        }
        public bool DeleteCategoryById(int id)
        {
            Categories cat = _Categories.SingleOrDefault((Categories cat) => cat.Id == id);

            if (cat == null)
            {
                return false;
            }
            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
        public bool AddCategories(string name)
        {
            Categories cat = ToEntity(name);
            _Categories.Add(cat);
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
        public Categories ToEntity(string name)
        {
            return new Categories
            {
                Name = name
            };
        }
    }
}
