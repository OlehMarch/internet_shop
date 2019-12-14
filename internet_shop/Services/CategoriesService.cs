using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using internet_shop.Models;

namespace internet_shop.Services
{
    public class CategoriesService
    {
        public CategoriesService(BaseDbContext db)
        {
            _db = db;
        }

        private readonly BaseDbContext _db;
        private DbSet<Categories> Categories => _db.Categories;

        public List<Categories> GetAllCategory()
        {
            return Categories.ToList();
        }

        public Categories GetCategoryById(int id)
        {
            var cat = Categories.SingleOrDefault((Categories cat) => cat.Id == id);
            if (cat == null)
            {
                return null;
            }
            return cat;
        }
        public bool DeleteCategoryById(int id)
        {
            Categories cat = Categories.SingleOrDefault((Categories cat) => cat.Id == id);

            if (cat == null)
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
        public bool AddCategories(string name)
        {
            Categories cat = ToEntity(name);
            Categories.Add(cat);
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
                Name = name,
            };
        }
    }
}
