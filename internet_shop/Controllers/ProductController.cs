using Microsoft.AspNetCore.Mvc;

using internet_shop.Models;
using internet_shop.Services;
using System.Collections.Generic;

namespace internet_shop.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        private readonly ProductService _productService;

        [HttpGet("{id}")]//Product/1
        public IActionResult GetById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound("Bad request");
            else
                return Ok(product);
        }

        [HttpGet]//Product/
        public IActionResult GetAll()
        {
            var product = _productService.GetAll();
            if (product == null)
                return NotFound("Bad request");
            else
                return Ok(product);
        }
        
        [HttpGet("/filter")]
        public IEnumerable<Product> Filter([FromQuery] List<int> brandIds, [FromQuery] List<int> categoryIds)
        {
            // TODO(friday13): where Filter method of ProductService?

            //return _productService.Filter(brandIds, categoryIds);
            return new Product[0];
        }

        [HttpDelete("{id}")]
        public IActionResult DeletebyId(int id)
        {
            var products = _productService.Remove(id);
            if (products == true)
                return Ok("Is Deleted");
            else
                return NotFound("Fault");
        }

        [HttpPut]
        public IActionResult UpdateProductToPromos([FromBody] Product value)
        {
            var product = _productService.UpdateProduct(value.Id,value.Name, value.Description, value.BrandId, value.CategoryId, value.Price);
            if (product == null)
                return BadRequest("400");
            else
                return Ok("200");

        }

        [HttpPost("/add")]//add?Name=Greta&&Description=SD
        public IActionResult AddById([FromBody] Product value)
        {
            var product = _productService.AddNewProduct(value.Name, value.Description, value.BrandId, value.CategoryId, value.Price);
            if (product == null)
                return NotFound("Bad Request");
            else
                return Ok("New produc is added");
        }
    }
}
