using internet_shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using internet_shop.DbContexts;

namespace internet_shop.Services
{
    public class CategoriesService
    {

        public CategoriesService(CategoryDbContext db)
        {
            _db = db;
        }

        private CategoryDbContext _db;
        private DbSet<Categories> _cat => _db.Categories;

        public List<Categories> GetAllCategory()
        {
            return _cat.ToList();
        }

        public Categories GetCategoryById(int id)
        {
            var cat = _cat.SingleOrDefault((Categories cat) => cat.Id == id);
            if (cat == null)
            {
                return null;
            }
            return cat;
        }
        public bool DeleteCategoryById(int id)
        {
            Categories cat = _cat.SingleOrDefault((Categories cat) => cat.Id == id);

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
            _cat.Add(cat);
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
