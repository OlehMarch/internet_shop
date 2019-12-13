using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace internet_shop.Models
{
    public class CartProduct
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public CartProduct(int productId, string cartId)
        {
            this.CartId = cartId;
            this.ProductId = productId;
        }
    }
}
