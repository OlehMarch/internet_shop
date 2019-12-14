using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using internet_shop.Models;
using internet_shop.DbContexts;


namespace internet_shop.Services
{
    public class PromosForBrandService
    {        
        public PromosForBrandService(PromosForBrandDbContext db)
        {
            _db = db;
        }
        
        private readonly PromosForBrandDbContext _db;
        private DbSet<PromosForBrandModel> PromosForBrandModel => _db.PromosForBrandModel;

        public List<PromosForBrandModel> GetAllPromosForBrand()
        {
            return PromosForBrandModel.ToList();
        }

        public PromosForBrandModel GetPromoForBrandById(int id)
        {
            var promosForBrand = PromosForBrandModel.SingleOrDefault((PromosForBrandModel promosForBrand) => promosForBrand.Id == id);
            if (promosForBrand == null)
            {
                return null;
            }
            return promosForBrand;
        }
        public (PromosForBrandModel promosForBrand, Exception exception) DeletePromoForBrandById(int id)
        {
            PromosForBrandModel promosForBrand = PromosForBrandModel.SingleOrDefault((PromosForBrandModel promos) => promos.Id == id);
            if (promosForBrand == null)
            {
                return (null, new ArgumentNullException($"promos with id: {promosForBrand.Id} not found"));
            }

            if (promosForBrand.Id != 0)
            {
                promosForBrand.Enabled = false;
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return (null, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }

            return (promosForBrand, null);

            /*PromosForBrandModel promosForBrand = PromosForBrandModel.SingleOrDefault((PromosForBrandModel promos) => promos.Id == id);

            if (promosForBrand == null)
            {
                return (false, new ArgumentNullException($"Promo for brand with id: {id} not found"));
            }

            EntityEntry<PromosForBrandModel> result = PromosForBrandModel.Remove(promosForBrand);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return (false, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }

            return (result.State == EntityState.Deleted, null);*/
        }
        public PromosForBrandModel AddPromoForBrand(int promoId, string name, int universalId, bool enabled/*, int BrandId*/)
        {
            PromosForBrandModel promosForBrand = ToEntity(promoId, name, universalId, enabled/*, int BrandId*/);
            PromosForBrandModel.Add(promosForBrand);
            try
            {
                _db.SaveChanges();
            }
            catch
            {
                return null;
            }

            return promosForBrand;
        }
        public PromosForBrandModel ToEntity(int promoId, string name, int universalId, bool enabled/*, int brandId*/)
        {
            return new PromosForBrandModel
            {
                PromoId = promoId,
                Name = name,
                UniversalId = universalId,
                Enabled = enabled,
                /*BrandId = brandId,*/
            };
        }
        public (PromosForBrandModel promosForBrand, Exception exception) UpdatepromoForBrand(PromosForBrandModel _promosForBrand)
        {
            PromosForBrandModel promosForBrand = this.PromosForBrandModel.SingleOrDefault((PromosForBrandModel promosForBrand) => promosForBrand.Id == _promosForBrand.Id);

            if (promosForBrand == null)
            {
                return (null, new ArgumentNullException($"promos with id: {_promosForBrand.Id} not found"));
            }

            if (_promosForBrand.Id != 0)
            {
                promosForBrand.PromoId = _promosForBrand.PromoId;
                promosForBrand.Name = _promosForBrand.Name;
                promosForBrand.UniversalId = _promosForBrand.UniversalId;
                /*promosForBrand.BrandId = _promosForBrand.BrandId;*/
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return (null, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }

            return (_promosForBrand, null);
        }
    }
}
