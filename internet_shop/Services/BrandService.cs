using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using internet_shop.Models;
using internet_shop.DbContexts;

namespace internet_shop.Services
{
    public class BrandService
    {
        public BrandService(BrandDbContext db)
        {
            _db = db;
        }

        private readonly BrandDbContext _db;
        private DbSet<Brand> Brand => _db.Brands;

        public List<Brand> GetAllBrand()
        {
            return Brand.ToList();
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
        public (bool result, Exception exception) DeleteBrandById(int id)
        {
            Brand brand = Brand.SingleOrDefault((Brand brand) => brand.Id == id);

            if (brand == null)
            {
                return (false, new ArgumentNullException($"Brand with specific id: {id} not found"));
            }

            EntityEntry<Brand> result = Brand.Remove(brand);

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
        public Brand AddBrand(string name)
        {
            Brand brand = ToEntity(name);
            Brand.Add(brand);
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
        public Brand ToEntity(string name)
        {
            return new Brand
            {
                Name = name
            };
        }
    }
}
