using internet_shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using internet_shop.DbContexts;

namespace internet_shop.Services
{
    public class PromosService
    {
        public PromosService(PromoDbContext db)
        {
            _db = db;
        }

        private PromoDbContext _db;
        private DbSet<Promos> _promos => _db.Promos;

        public List<Promos> GetAllPromos()
        {
            return _promos.ToList();
        }

        public Promos GetPromoById(int id)
        {
            var promos = _promos.SingleOrDefault((Promos promos) => promos.Id == id);
            if (promos == null)
            {
                return null;
            }
            return promos;
        }
        public (bool result, Exception exception) DeletePromoById(int id)
        {
            Promos promos = _promos.SingleOrDefault((Promos promos) => promos.Id == id);

            if (promos == null)
            {
                return (false, new ArgumentNullException($"Promo with id: {id} not found"));
            }

            EntityEntry<Promos> result = _promos.Remove(promos);

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
        public Promos AddPromo(string name, int value)
        {
            Promos promo = ToEntity(name, value);
            _promos.Add(promo);
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
        public Promos ToEntity(string name, int value)
        {
            return new Promos
            {
                Name = name,
                Value = value,
            };
        }
        public (Promos promos, Exception exception) Updatepromos(Promos _promos)
        {
            Promos promos = this._promos.SingleOrDefault((Promos promos) => promos.Name == _promos.Name);

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
