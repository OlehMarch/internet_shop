using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using internet_shop.Dto;
using internet_shop.Models;

namespace internet_shop.Services
{
    public class BrandService
    {
        public BrandService(BaseDbContext db)
        {
            _db = db;
        }

        private readonly BaseDbContext _db;
        private DbSet<Brand> Brand => _db.Brands;

        public BrandDTO ToBrandDto(Brand brand)
        {
            if (brand == null) return null;

            return new BrandDTO
            {
                Id = brand.Id,
                Name = brand.Name,
                Value = CalculateProductCount(brand)
            };
        }

        public int CalculateProductCount(Brand brand)
        {
            int count = 0;
            var product = _db.Products.ToList();
            for (int i = 0; i < product.Count; i++)
            {
                if (product[i].BrandId == brand.Id) count++;
            }
            return count;
        }

        public List<BrandDTO> GetAllBrand()
        {
            List<BrandDTO> list = new List<BrandDTO>();
            var brandList = Brand.ToList();
            for (int i = 0; i < brandList.Count; i++)
            {
                list.Add(ToBrandDto(brandList[i]));
            }
            return list;
        }

        public BrandDTO GetBrandById(int id)
        {
            var brand = Brand.SingleOrDefault((Brand brand) => brand.Id == id);
            if (brand == null)
            {
                return null;
            }

            var brandDto = ToBrandDto(brand);
            return brandDto;
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

        public BrandDTO AddBrand(string name)
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

            return ToBrandDto(brand);
        }

        public Brand ToEntity(string name)
        {
            return new Brand
            {
                Name = name
            };
        }

        public (BrandDTO brandDto, Exception exception) UpdateBrand(Brand _brand)
        {
            Brand brand = this.Brand.SingleOrDefault((Brand brand) => brand.Id == _brand.Id);
            if (brand == null)
            {
                return (null, new ArgumentException($"brand with id:{_brand.Id}not found"));

            }
            if (_brand.Id != 0) brand.Name = _brand.Name;

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