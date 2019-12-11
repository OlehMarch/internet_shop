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

        public CategoriesService(CategoryDbContext db)
        {
            _db = db;
        }

        private readonly CategoryDbContext _db;
        private DbSet<Categories> Cat => _db.Categories;

        public List<Categories> GetAllCategory()
        {
            return Cat.ToList();
        }

        public Categories GetCategoryById(int id)
        {
            var cat = Cat.SingleOrDefault((Categories cat) => cat.Id == id);
            if (cat == null)
            {
                return null;
            }
            return cat;
        }
        public bool DeleteCategoryById(int id)
        {
            Categories cat = Cat.SingleOrDefault((Categories cat) => cat.Id == id);

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
        public bool AddCategories(string name, int value)
        {
            Categories cat = ToEntity(name, value);
            Cat.Add(cat);
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
        public Categories ToEntity(string name, int value)
        {
            return new Categories
            {
                Name = name,
                Value = value,
            };
        }
    }
}
