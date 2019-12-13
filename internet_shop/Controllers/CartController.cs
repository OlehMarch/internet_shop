using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using internet_shop.Models;
using internet_shop.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace internet_shop.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService cartService;    
        public CartController(CartService _cartService)
        {
            cartService = _cartService;
            
        }
        [HttpPost("api/getCart")]
        public void GetCart([FromQuery] string cartId, [FromQuery] string profileId, [FromQuery] string address)
        {
            cartService.GetCart(cartId, profileId, address);
        }

        [HttpPost("api/addToCart")]
        public void AddToCart([FromQuery] int productId, [FromQuery] string cartId)
        {
             cartService.Add(productId, cartId);
        }
        [HttpPost("api/removeFromCart")]
        public void RemoveFromCart([FromQuery] int productId, [FromQuery] string cartId)
        {
            cartService.Remove(productId, cartId);
        }
        [HttpPost("api/clearAllFromCart")]
        public void ClearCart([FromQuery] string cartId)
        {
            cartService.Clear(cartId);
        }
        [HttpPost("api/pay")]
        public void PayCart([FromQuery] string cartId)
        {
            cartService.Pay(cartId);
        }


        //// GET: Cart/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Cart/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Cart/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Cart/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Cart/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Cart/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Cart/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}