using System.Linq;
using System.Collections.Generic;
using internet_shop.Models;
using Microsoft.EntityFrameworkCore;
using internet_shop.Dto;

namespace internet_shop.Services
{
    public class ProductService
    {
        public ProductService(BaseDbContext db, BrandService brandService, PromosService promosService, CategoriesService categoriesService)
        {
            _db = db;
            _brandService = brandService;
            _categoriesService = categoriesService;
            _promosService = promosService;
        }
        private readonly PromosService _promosService;
        private readonly BrandService _brandService;
        private readonly CategoriesService _categoriesService;
        private readonly BaseDbContext _db;
        private DbSet<Product> Products => _db.Products;
        private DbSet<Brand> Brands => _db.Brands;
        private DbSet<Categories> Categories => _db.Categories;
        private DbSet<Promos> Promos => _db.Promos;

        public ProductDTO AddNewProduct(string name, string description, int brandId, int categoryId, int price)
        {
            var product = ToEntity(name, description, brandId, categoryId, price);
            if (product == null) return null;

            Products.Add(product);

            try { _db.SaveChanges(); }
            catch { return null; }

            return ToDTO(product);
        }
        public List<ProductDTO> GetAll()
        {
            List<ProductDTO> product = new List<ProductDTO>(); 
            var productList = Products.ToList();
            for (int i = 0; i < productList.Count; i++) { product.Add(ToDTO(productList[i])); }

            if (product.Count == 0) return null;
            return product;
        }

        public ProductDTO UpdateProduct(int id, string name, string description, int brandId, int categoryId, int price)
        {
            var product = Products.Find(id);
            product = ToEntity(name, description, brandId, categoryId, price);
            _db.Update(product);
            if (product == null) return null;
            return ToDTO(product);
        }

        public bool Remove(int id)
        {
            var products = Products.Find(id);
            if (products == null)
                return false;
            else
            {
                Products.Remove(products);
                _db.SaveChanges();
                return true;
            }
        }

        public ProductDTO GetProductById(int id)
        {
            if (Products.Find(id) == null) return null;
                else return ToDTO(Products.Find(id));
        }

        public Product ToEntity(string name, string description, int brandId, int categoryId, int price)
        {
            return new Product
            {
                Name = name,
                Description = description,
                BrandId = brandId,
                CategoryId = categoryId,
                Price = price
            };
        }

        public ProductDTO ToDTO(Product product)
        {
            var persent = EntryPromos(product.Id).Item1;
            if (product == null) return null;

            return new ProductDTO
            {
                Id = product.Id,
                brandDto = _brandService.ToBrandDto(Brands.Find(product.BrandId)),
                categoriesDto = _categoriesService.ToDTO(Categories.Find(product.CategoryId)),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                UpdatedPrice = product.Price - (persent * (product.Price / 100)),
                PromosDto = EntryPromos(product.Id).Item2
            };
        }

        public (int, PromosDTO) EntryPromos(int id)
        {
            var product = Products.SingleOrDefault((Product product) => product.Id == id);
            var promos = _db.Promos.Where((x) => x.IsEnabled == true).ToList();

            return ForToPromos(promos, product);
        }

        public (int, PromosDTO) ForToPromos(List<Promos> promos, Product product)
        {
            List<Promos> promoList = new List<Promos>();
            for (int item = 0; item < promos.Count; item++)
            {
                if (product.CategoryId == promos[item].CategoryId ||
                    product.BrandId == promos[item].BrandId ||
                    product.Id == promos[item].ProductId)
                {
                    promoList.Add(promos[item]);
                }
            }
            if (promoList.Count == 0)
            {
                return (0,null);
            }
            else
            {
                var promosObj = promoList.Max(x => x);
                int promo = promoList.Max(x => x.Value);
                return (promo,_promosService.ToDTO(promosObj));
            }
        }
    }
}
