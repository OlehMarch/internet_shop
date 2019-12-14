using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using internet_shop.Models;
using internet_shop.DbContexts;


namespace internet_shop.Services
{
    public class PromosForCategoriesService
    {
        public PromosForCategoriesService()
        {

        }
        public PromosForCategoriesService(PromosForCategoriesDbContext db)
        {
            _db = db;
        }
        
        private readonly PromosForCategoriesDbContext _db;
        private DbSet<PromosForCategoriesModel> PromosForCategoriesModel => _db.PromosForCategoriesModel;

        public List<PromosForCategoriesModel> GetAllPromosForCategories()
        {
            return PromosForCategoriesModel.ToList();
        }

        public PromosForCategoriesModel GetPromoForCategoriesById(int id)
        {
            var promosForCategories = PromosForCategoriesModel.SingleOrDefault((PromosForCategoriesModel promosForCategories) => promosForCategories.Id == id);
            if (promosForCategories == null)
            {
                return null;
            }
            return promosForCategories;
        }
        public (PromosForCategoriesModel promosForCategories, Exception exception) DeletePromoForCategoriesById(int id)
        {
            PromosForCategoriesModel promosForCategories = PromosForCategoriesModel.SingleOrDefault((PromosForCategoriesModel promos) => promos.Id == id);
            if (promosForCategories == null)
            {
                return (null, new ArgumentNullException($"promos with id: {promosForCategories.Id} not found"));
            }

            if (promosForCategories.Id != 0)
            {
                promosForCategories.Enabled = false;
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return (null, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }

            return (promosForCategories, null);

            /*PromosForCategoriesModel promosForCategories = PromosForCategoriesModel.SingleOrDefault((PromosForCategoriesModel promos) => promos.Id == id);

            if (promosForCategories == null)
            {
                return (false, new ArgumentNullException($"Promo for brand with id: {id} not found"));
            }

            EntityEntry<PromosForCategoriesModel> result = PromosForCategoriesModel.Remove(promosForCategories);

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
        public PromosForCategoriesModel AddPromoForCategories(int promoId, string name, int universalId,/*, int BrandId*/bool enabled)
        {
            PromosForCategoriesModel promosForCategories = ToEntity(promoId, name, universalId/*, int BrandId*/, enabled);
            PromosForCategoriesModel.Add(promosForCategories);
            try
            {
                _db.SaveChanges();
            }
            catch
            {
                return null;
            }

            return promosForCategories;
        }
        public PromosForCategoriesModel ToEntity(int promoId, string name, int universalId/*, int categoryId*/, bool enabled)
        {
            return new PromosForCategoriesModel
            {
                PromoId = promoId,
                Name = name,
                UniversalId = universalId,
                Enabled = enabled
                //CategoryId = categoryId
            };
        }
        public (PromosForCategoriesModel promosForCategories, Exception exception) UpdatepromoForCategories(PromosForCategoriesModel _promosForCategories)
        {
            PromosForCategoriesModel promosForCategories = this.PromosForCategoriesModel.SingleOrDefault((PromosForCategoriesModel promosForCategories) => promosForCategories.Id == _promosForCategories.Id);

            if (promosForCategories == null)
            {
                return (null, new ArgumentNullException($"PromosForCategoriesModel with id: {_promosForCategories.Id} not found"));
            }

            if (_promosForCategories.Id != 0)
            {
                promosForCategories.PromoId = _promosForCategories.PromoId;
                promosForCategories.Name = _promosForCategories.Name;
                promosForCategories.UniversalId = _promosForCategories.UniversalId;
                promosForCategories.Enabled = _promosForCategories.Enabled;
                //promosForCategories.CategoryId = _promosForCategories.CategoryId;
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return (null, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }

            return (_promosForCategories, null);
        }
    }
}
