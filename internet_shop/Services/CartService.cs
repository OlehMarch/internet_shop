using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using internet_shop.Models;

namespace internet_shop.Services
{
    public class CartService
    {
        private readonly BaseDbContext _context;

        public CartService(BaseDbContext dBContent)
        {
            _context = dBContent;
        }

        public dynamic GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            string _cartId = session.GetString("cartId") ?? Guid.NewGuid().ToString();
            session.SetString("cartId", _cartId);
            Cart cart = new Cart(_cartId);
            if (String.IsNullOrEmpty(session.GetString("profileId")))
            {
                // _context.Carts.Add(cart); // TODO(artemmal): DB Key too long, cannot create db
            }
            else
            {
                cart.ProfileId = session.GetString("profileId");
                //_context.Carts.Add(cart); // TODO(artemmal): DB Key too long, cannot create db
            }
            try
            {
                _context.SaveChanges();

            }
            catch
            {
                return (null);
            }
            return cart;
        }

        public dynamic GetCart(string cartId, string profileId, string address)
        {
            Cart cart = new Cart() { CartId = cartId, ProfileId = profileId, Address = address };
            // _context.Carts.Add(cart); // TODO(artemmal): DB Key too long, cannot create db
            try
            {
                _context.SaveChanges();

            }
            catch
            {
                return (null);
            }
            return cart;
        }

        public bool Add(int productId, string cartId)
        {
            //string _cartId = Guid.NewGuid().ToString();
            //if (appDBContent.Carts.SingleOrDefault(cart => cart.CartId == cartId) == null)
            //{
            //    appDBContent.Carts.Add(new Cart(profileId, cartId));
            //    appDBContent.SaveChanges();
            //}
            _context.CartProducts.Add(new CartProduct(productId, cartId));
            try
            {
                _context.SaveChanges();

            }
            catch
            {
                return (false);
            }
            return true;
        }

        public bool Remove(int productId, string cartId)
        {
            var p = _context.CartProducts.FirstOrDefault(prod => prod.ProductId == productId && prod.CartId == cartId);
            _context.CartProducts.Remove(p);
            try
            {
                _context.SaveChanges();

            }
            catch
            {
                return (false);
            }
            return true;
        }

        public bool Clear(string cartId)
        {
            while (_context.CartProducts.FirstOrDefault(prod => prod.CartId == cartId) != null)
            {
                var product = _context.CartProducts.FirstOrDefault(prod => prod.CartId == cartId);
                _context.CartProducts.Remove(product);
                try
                {
                    _context.SaveChanges();

                }
                catch
                {
                    return (false);
                }
            }

            return true;
        }

        public bool Pay(string cartId)
        {
            bool payStatus = false;
            // TODO(artemmal): DB Key too long, cannot create db
            //Cart cart = _context.Carts.SingleOrDefault(cart => cart.CartId == cartId);
            //List<CartProduct> products = _context.CartProducts.Where(product => product.CartId == cartId).ToList();
            //if (cart != null && products != null)
            //{
            //    payStatus = true;
            //}
            return payStatus;
        }
    }
}
