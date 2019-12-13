using internet_shop.Controllers;
using internet_shop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace internet_shop.Service
{
    public class CartService
    {
        private AppDBContent appDBContent;
        
        public CartService(AppDBContent dBContent)
        {
            this.appDBContent = dBContent;
        }
        
        public void GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            string _cartId = session.GetString("cartId") ?? Guid.NewGuid().ToString();
            session.SetString("cartId", _cartId);
            Cart cart = new Cart(_cartId);
            if (String.IsNullOrEmpty(session.GetString("profileId")))
            {
                appDBContent.Carts.Add(cart);
            }
            else
            {
                cart.ProfileId = session.GetString("profileId");
                appDBContent.Carts.Add(cart);
            }
            appDBContent.SaveChanges();
        }

        public void GetCart(string cartId, string profileId, string address)
        {
            Cart cart = new Cart() { CartId = cartId, ProfileId = profileId, Address = address };
            appDBContent.Carts.Add(cart);
            appDBContent.SaveChanges();
        }

        public void Add(int productId, string cartId) 
        {
            //string _cartId = Guid.NewGuid().ToString();
            //if (appDBContent.Carts.SingleOrDefault(cart => cart.CartId == cartId) == null)
            //{
            //    appDBContent.Carts.Add(new Cart(profileId, cartId));
            //    appDBContent.SaveChanges();
            //}
            appDBContent.CartProducts.Add(new CartProduct(productId, cartId));
            appDBContent.SaveChanges();
        }
        public void Remove(int productId, string cartId) {
            var p = appDBContent.CartProducts.FirstOrDefault(prod => prod.ProductId == productId && prod.CartId == cartId);
            appDBContent.CartProducts.Remove(p);
            appDBContent.SaveChanges();
        }
        public void Clear(string cartId) {
            while (appDBContent.CartProducts.SingleOrDefault(prod => prod.CartId == cartId) != null)
            {
                var p = appDBContent.CartProducts.FirstOrDefault(prod => prod.CartId == cartId);
                appDBContent.CartProducts.Remove(p);
                appDBContent.SaveChanges();
            }
        }
        public bool Pay(string cartId) {
            bool payStatus = false;
            Cart cart = appDBContent.Carts.SingleOrDefault(cart => cart.CartId == cartId);
            List<CartProduct> products = appDBContent.CartProducts.Where(product => product.CartId == cartId).ToList();
            if (cart != null && products == null)
            {
                payStatus = true;
            }
            return payStatus;
        }
    }
}
