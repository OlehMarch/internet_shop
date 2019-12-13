using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace internet_shop.Models
{
    public class Cart
    {
        public Cart()
        {

        }
        public Cart(string cartId)
        {
            this.CartId = cartId;
        }
        public string CartId { get; set; }
        public string ProfileId { get; set; }
        public string Address { get; set; }
        //public List<CartProduct> Products { get; set; }

    }
}
