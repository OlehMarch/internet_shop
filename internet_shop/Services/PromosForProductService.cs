using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using internet_shop.Models;
using internet_shop.DbContexts;


namespace internet_shop.Services
{
    public class PromosForProductService
    {
        public PromosForProductService()
        {

        }
        public PromosForProductService(PromosForProductDbContext db)
        {
            _db = db;
        }
        
        private readonly PromosForProductDbContext _db;
        private DbSet<PromosForProductModel> PromosForProductModel => _db.PromosForProductModel;

        public List<PromosForProductModel> GetAllPromosForProduct()
        {
            return PromosForProductModel.ToList();
        }

        public PromosForProductModel GetPromoForProductById(int id)
        {
            var promosForProduct = PromosForProductModel.SingleOrDefault((PromosForProductModel promosForProduct) => promosForProduct.Id == id);
            if (promosForProduct == null)
            {
                return null;
            }
            return promosForProduct;
        }
        public (PromosForProductModel promosForProduct, Exception exception) DeletePromoForProductById(int id)
        {
            PromosForProductModel promosForProduct = PromosForProductModel.SingleOrDefault((PromosForProductModel promos) => promos.Id == id);
            if (promosForProduct == null)
            {
                return (null, new ArgumentNullException($"PromosForProductModel with id: {promosForProduct.Id} not found"));
            }

            if (promosForProduct.Id != 0)
            {
                promosForProduct.Enabled = false;
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return (null, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }

            return (promosForProduct, null);

            /*PromosForProductModel promosForProduct = PromosForProductModel.SingleOrDefault((PromosForProductModel promos) => promos.Id == id);

            if (promosForProduct == null)
            {
                return (false, new ArgumentNullException($"Promo for brand with id: {id} not found"));
            }

            EntityEntry<PromosForProductModel> result = PromosForProductModel.Remove(promosForProduct);

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
        public PromosForProductModel AddPromoForProduct(int promoId, string name, int universalId/*, int productId*/, bool Enabled)
        {
            PromosForProductModel promosForProduct = ToEntity(promoId, name, universalId/*, int productId*/, Enabled);
            PromosForProductModel.Add(promosForProduct);
            try
            {
                _db.SaveChanges();
            }
            catch
            {
                return null;
            }

            return promosForProduct;
        }
        public PromosForProductModel ToEntity(int promoId, string name, int universalId/*, int productId*/, bool Enabled)
        {
            return new PromosForProductModel
            {
                PromoId = promoId,
                Name = name,
                UniversalId = universalId,
                Enabled = Enabled
                //ProductId = productId
            };
        }
        public (PromosForProductModel promosForProduct, Exception exception) UpdatepromoForProduct(PromosForProductModel _promosForProduct)
        {
            PromosForProductModel promosForProduct = this.PromosForProductModel.SingleOrDefault((PromosForProductModel promosForProduct) => promosForProduct.Id == _promosForProduct.Id);

            if (promosForProduct == null)
            {
                return (null, new ArgumentNullException($"PromosForProductModel with id: {_promosForProduct.Id} not found"));
            }

            if (_promosForProduct.Id != 0)
            {
                promosForProduct.PromoId = _promosForProduct.PromoId;
                promosForProduct.Name = _promosForProduct.Name;
                promosForProduct.UniversalId = _promosForProduct.UniversalId;
                promosForProduct.Enabled = _promosForProduct.Enabled;
                // promosForProduct.ProductId = _promosForProduct.ProductId;
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                return (null, new DbUpdateException($"Cannot save changes: {e.Message}"));
            }

            return (_promosForProduct, null);
        }
    }
}
