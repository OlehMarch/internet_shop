using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace internet_shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Promo { get; set; }
    }
}
