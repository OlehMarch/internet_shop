using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using internet_shop.Models;
using internet_shop.DbContexts;

namespace internet_shop.Services
{
    public class PromosService
    {
        public PromosService(PromosDbContext db)
        {
            _db = db;
        }

        private readonly PromosDbContext _db;
        private DbSet<Promos> Promos => _db.Promos;

        public List<Promos> GetAllPromos()
        {
             return Promos.ToList();
        }

        public Promos GetPromoById(int id)
        {
            var promos = Promos.SingleOrDefault((Promos promos) => promos.Id == id);
            if (promos == null)
            {
                return null;
            }
            return promos;
        }
        public (bool result, Exception exception) DeletePromoById(int id)
        {
            Promos promos = Promos.SingleOrDefault((Promos promos) => promos.Id == id);

            if (promos == null)
            {
                return (false, new ArgumentNullException($"Promo with id: {id} not found"));
            }

            EntityEntry<Promos> result = Promos.Remove(promos);

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
        public Promos AddPromo(string name, int value, int categoryId, int brandId,int productId,bool isEnabled)
        {
                var promos = ToEntity(name, value, categoryId, brandId, productId, isEnabled);
                if (promos == null)
                    return null;
                else
                {
                    Promos.Add(promos);
                    _db.SaveChanges();
                    return promos;
                }
            //Promos promo = ToEntity(name, value, categoryId, brandId, productId, isEnabled);
            //Promos.Add(promo);
            //try
            //{
            //    _db.SaveChanges();
            //}
            //catch
            //{
            //    return null;
            //}

            //return promo;
        }
        public Promos ToEntity(string name, int value,int categoryId, int brandId, int productId, bool isEnabled)
        {
            return new Promos
            {
                Name = name,
                Value = value,
                CategoryId = categoryId,
                BrandId = brandId,
                ProductId = productId,
                IsEnabled = isEnabled
            };
        }
        public (Promos promos, Exception exception) Updatepromos(Promos _promos)
        {
            Promos promos = Promos.SingleOrDefault((Promos promos) => promos.Name == _promos.Name);

            if (promos == null)
            {
                return (null, new ArgumentNullException($"promos with id: {_promos.Id} not found"));
            }

            if (_promos.Name != null)
            {
                promos.Value = _promos.Value;
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return (null, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }

            return (_promos, null);
        }
    }
}