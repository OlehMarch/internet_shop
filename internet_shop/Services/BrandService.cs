using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using internet_shop.Models;

namespace internet_shop.Services
{
    public class BrandService
    {
        private readonly BaseDbContext _db;
        public BrandService(BaseDbContext db)
        {
            _db = db;
        }

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
                Name = name,
            };
        }

        public (Brand brand, Exception exception) UpdateBrand(Brand _brand)
        {
            Brand brand = this.Brand.SingleOrDefault((Brand brand) => brand.Id == _brand.Id);
            if (brand == null)
            {
                return (null, new ArgumentException($"brand with id:{_brand.Id}not found"));

            }
            if (_brand.Id != 0)
            {
                brand.Name = _brand.Name;
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return (null, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }
            return (_brand, null);
        }
    }
}