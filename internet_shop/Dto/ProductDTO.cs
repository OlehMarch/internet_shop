using internet_shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace internet_shop.Dto
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public BrandDTO brandDto{ get; set; }
        public CategoriesDTO categoriesDto { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int UpdatedPrice { get; set; }
        public PromosDTO PromosDto { get; set; }
    }
}
