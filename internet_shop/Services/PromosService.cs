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
        public PromosService(PromosDbContext db, 
            PromosForBrandService brandsService, 
            PromosForProductService productsService,
            PromosForCategoriesService categoriesService
            /*, ProductDbContext productDbContext*/)
        {
            _db = db;
            _brandsService = brandsService;
            _productsService = productsService;
            _categoriesService = categoriesService;
            //_productDbContext = productDbContext;
        }
        //private readonly ProductDbContext _productDbContext;
        private readonly PromosDbContext _db;
        private readonly PromosForBrandService _brandsService;
        private readonly PromosForProductService _productsService;
        private readonly PromosForCategoriesService _categoriesService;

        //private DbSet<Product> _Dataproducts => _productDbContext.Products;
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
        public Promos AddPromo(string name, int value, int universalId,
            int ProductId, int BrandId, int Category, bool IsEnabled)
        {
            Promos promo = ToEntity(name, value, universalId, ProductId, BrandId, Category, IsEnabled);

            if (name.Contains("brand"))//(promo.BrandId != 0)
            {
                //string brandName = "brand";
                _brandsService.AddPromoForBrand(promo.Id, promo.Name/*brandName*/, promo.UniversalId, promo.IsEnabled);
            }
            if (name.Contains("product"))//(promo.ProductId != 0)
            {
                //string brandName = "product";
                _productsService.AddPromoForProduct(promo.Id, promo.Name/*brandName*/, promo.UniversalId, promo.IsEnabled);
            }
            if (name.Contains("categori"))//(promo.Category != 0)
            {
                //string brandName = "categori";
                _categoriesService.AddPromoForCategories(promo.Id, promo.Name/*brandName*/, promo.UniversalId, promo.IsEnabled);
            }
            //Product datadb = _Dataproducts.SingleOrDefault((Product product) => product.Name == name); 


            Promos.Add(promo);
            try
            {
                _db.SaveChanges();
            }
            catch
            {
                return null;
            }

            return promo;
        }
        public Promos ToEntity(string name, int value, int universalId, int productId, int brandId, int categoryId, bool isEnabled)
        {
            return new Promos
            {
                Name = name,
                Value = value,
                UniversalId = universalId,
                ProductId = productId,
                BrandId = brandId,
                CategoryId = categoryId,
                IsEnabled = isEnabled
            };
        }
        public (Promos promos, Exception exception) Updatepromos(Promos _promos)
        {
            Promos promos = this.Promos.SingleOrDefault((Promos promos) => promos.Id == _promos.Id);

            if (promos == null)
            {
                return (null, new ArgumentNullException($"promos with id: {_promos.Id} not found"));
            }

            if (_promos.Id != 0)
            {
                promos.Name = _promos.Name;
                promos.Value = _promos.Value;
                promos.UniversalId = _promos.UniversalId;
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