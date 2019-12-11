using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_shop.Models
{
    public class Product
    {
        public int id { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        List<int> promoId = new List<int>();
    }
}
