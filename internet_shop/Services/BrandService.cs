using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using shop.Models;

namespace shop.Services
{
    public class BrandService
    {
        public BrandService(AppDbContext db)
        {
            _db = db;
        }

        private AppDbContext _db;
        private DbSet<Brand> _brand => _db.Brand;

        public List<Brand> GetAllBrand()
        {
            return _brand.ToList();
        }

        public Brand GetBrandById(int id)
        {
            var brand = _brand.SingleOrDefault((Brand brand) => brand.Id == id);
            if (brand == null)
            {
                return null;
            }
            return brand;
        }
        public (bool result, Exception exception) DeleteBrandById(int id)
        {
            Brand brand = _brand.SingleOrDefault((Brand brand) => brand.Id == id);

            if (brand == null)
            {
                return (false, new ArgumentNullException($"Brand with specific id: {id} not found"));
            }

            EntityEntry<Brand> result = _brand.Remove(brand);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return (false, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }

            return (result.State == EntityState.Deleted, null);
        }
        public Brand AddBrand(string name, int value)
        {
            Brand brand = ToEntity(name, value);
            _brand.Add(brand);
            try
            {
                _db.SaveChanges();
            }
            catch
            {
                return null;
            }

            return brand;
        }
        public Brand ToEntity(string name, int value)
        {
            return new Brand
            {
                Name = name,
                Value = value,
            };
        }
    }
}
