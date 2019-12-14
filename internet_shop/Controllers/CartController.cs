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
        [HttpPost("/getCart")]
        public IActionResult GetCart([FromQuery] string cartId, [FromQuery] string profileId, [FromQuery] string address)
        {
            var data = cartService.GetCart(cartId, profileId, address);
            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost("/addToCart")]
        public IActionResult AddToCart([FromQuery] int productId, [FromQuery] string cartId)
        {
            var data = cartService.Add(productId, cartId);
            if (data == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }
        [HttpPost("/removeFromCart")]
        public IActionResult RemoveFromCart([FromQuery] int productId, [FromQuery] string cartId)
        {
            var data = cartService.Remove(productId, cartId);
            if (data == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }
        [HttpPost("/clearAllFromCart")]
        public IActionResult ClearCart([FromQuery] string cartId)
        {
            var data = cartService.Clear(cartId);
            if (data == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }
        [HttpPost("/pay")]
        public IActionResult PayCart([FromQuery] string cartId)
        {
            var data = cartService.Pay(cartId);
            if (data == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }
    }
}