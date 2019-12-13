using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using internet_shop.Models;
using internet_shop.Services;

namespace internet_shop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public BrandController(BrandService brandService)
        {
            _brandService = brandService;
        }

        private readonly BrandService _brandService;

        // GET: Brand
        [HttpGet]
        public List<Brand> Get()
        {
            return _brandService.GetAllBrand();
        }

        // GET: Brand/5
        [HttpGet("{id}")]

        public Brand GetById(int id)
        {
            // TODO(OleksandrRiznichenko): why removed null check?
            var brand = _brandService.GetBrandById(id);
            return brand;
        }

        [HttpGet("ProductById")]
        public List<Product> GetAllProductByBrand([FromBody] Product product)
        {
            // TODO(OleksandrRiznichenko): why removed null check?
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

        // DELETE: Brand/5
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
