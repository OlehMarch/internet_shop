using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using internet_shop.Models;
using internet_shop.Services;

namespace internet_shop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandService _brandService;

        public BrandController(BrandService brandService)
        {
            _brandService = brandService;
        }
        // GET: api/Brand
        [HttpGet]
        public List<Brand> Get()
        {
            return _brandService.GetAllBrand();
        }

        // GET: api/Brand/5
        [HttpGet("{id}")]

        public Brand GetById(int id)
        {
            var brand = _brandService.GetBrandById(id);
            return brand;
        }

        [HttpGet("ProductById")]
        public List<Product> GetAllProductByBrand([FromBody] Product product)
        {
            var products = _brandService.GetProductsByBrand(product.BrandId);
            return products;
        }

        // POST: Brand/Create
        [HttpPost]
        public IActionResult AddNewBrand([FromBody] Brand brand)
        {
            var brands = _brandService.AddBrand(brand.Name);

            if (brands == false)
            {
                return BadRequest("This is bad request");
            }
            else
            {
                return Ok("Ok, new brand added");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("/delete")]
        public ActionResult Delete(int id)
        {
            var brands = _brandService.DeleteBrandById(id);
            if (brands == false)
                return BadRequest("404");
            else
                return Ok("200");
        }
    }
}