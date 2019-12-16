using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using internet_shop.Models;
using internet_shop.Dto;

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

        public List<CategoriesDTO> GetAllCategory()
        {
            List<CategoriesDTO> categoriesList = new List<CategoriesDTO>(); 
            var categories = Categories.ToList();
            for (int i = 0; i < categories.Count; i++)
            {
                categoriesList.Add(ToDTO(categories[i]));
            }
            
            if (categoriesList.Count != 0) return categoriesList;
            else return null;
        }

        public CategoriesDTO GetCategoryById(int id)
        {
            var categories = Categories.SingleOrDefault((Categories categories) => categories.Id == id);
            
            if (categories == null) return null;
            else return ToDTO(categories);
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
        public CategoriesDTO AddCategories(string name)
        {
            Categories categories = ToEntity(name);
            Categories.Add(categories);

            try { _db.SaveChanges(); 
                return ToDTO(categories); }
            catch { return ToDTO(categories); }
        }
        public Categories ToEntity(string name)
        {
            return new Categories
            {
                Name = name,
            };
        }

        public CategoriesDTO ToDTO(Categories categories)
        {
            if (categories != null)
                return new CategoriesDTO
                {
                    Id = categories.Id,
                    Name = categories.Name,
                    Value = CalculateProductCount(categories)
                };
            else return null;
        }

        public int CalculateProductCount(Categories categories)
        {
            int count = 0;
            var product = _db.Products.ToList();
            for (int i = 0; i < product.Count; i++)
            {
                if (product[i].CategoryId == categories.Id) count++;
            }
            return count;
        }
    }
}
