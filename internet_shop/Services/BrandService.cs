using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using internet_shop.Models;
using internet_shop.Dto;

namespace internet_shop.Services
{
    public class BrandService
    {
        private readonly BaseDbContext _db;
        public BrandService(BaseDbContext db)
        {
            _db = db;
        }

        private DbSet<Brand> _brand => _db.Brands;

        public static BrandDto ToBrandDto(Brand brand)
        {
            if (brand == null)
            {
                return null;
            }
            return new BrandDto
            {
                Id = brand.Id,
                Name = brand.Name,
                Value = brand.Value
            };
        }

        public List<BrandDto> GetAllBrand()
        {
            List<BrandDto> list = new List<BrandDto>();
            var brandList = _brand.ToList();
            for (int i = 0; i < brandList.Count; i++)
            {
                list.Add(ToBrandDto(brandList[i]));
            }
            return list;
        }

        public BrandDto GetBrandById(int id)
        {
            var brand = _brand.SingleOrDefault((Brand brand) => brand.Id == id);
            if (brand == null)
            {
                return null;
            }
            else 
            {
                var brand1 = ToBrandDto(brand);
                return brand1;
            }
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
        public BrandDto AddBrand(string name, int value)
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

            return ToBrandDto(brand);
        }
        public Brand ToEntity(string name, int value)
        {
            return new Brand
            {
                Name = name, Value = value
            };
        }

        public (BrandDto brandDto, Exception exception) UpdateBrand(Brand _brand)
        {
            Brand brand = this._brand.SingleOrDefault((Brand brand) => brand.Id == _brand.Id);
            if (brand == null)
            {
                return (null, new ArgumentException($"brand with id:{_brand.Id}not found"));

            }
            if (_brand.Id != 0)
            {
                brand.Name = _brand.Name;
                brand.Value = _brand.Value;
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return (null, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }
            return (ToBrandDto(_brand), null);
        }
    }
}