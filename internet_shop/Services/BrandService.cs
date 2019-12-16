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

<<<<<<< Updated upstream
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
=======
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
            var brandList = _brand.ToList();
            for (int i = 0; i < brandList.Count; i++)
            {
                list.Add(ToBrandDto(brandList[i]));
            }
            return list;
        }

        public BrandDTO GetBrandById(int id)
        {
>>>>>>> Stashed changes
            var brand = _brand.SingleOrDefault((Brand brand) => brand.Id == id);
            if (brand == null)
            {
                return null;
            }
<<<<<<< Updated upstream
            else 
=======
            else
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        public BrandDto AddBrand(string name, int value)
        {
            Brand brand = ToEntity(name, value);
=======
        public BrandDTO AddBrand(string name)
        {
            Brand brand = ToEntity(name);
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
                Name = name, Value = value
            };
        }

        public (BrandDto brandDto, Exception exception) UpdateBrand(Brand _brand)
=======
                Name = name
            };
        }

        public (BrandDTO brandDto, Exception exception) UpdateBrand(Brand _brand)
>>>>>>> Stashed changes
        {
            Brand brand = this._brand.SingleOrDefault((Brand brand) => brand.Id == _brand.Id);
            if (brand == null)
            {
                return (null, new ArgumentException($"brand with id:{_brand.Id}not found"));

            }
<<<<<<< Updated upstream
            if (_brand.Id != 0)
            {
                brand.Name = _brand.Name;
                brand.Value = _brand.Value;
            }
=======
            if (_brand.Id != 0) brand.Name = _brand.Name;
>>>>>>> Stashed changes

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