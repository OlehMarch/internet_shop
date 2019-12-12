using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using internet_shop.Models;
using internet_shop.Services;

namespace internet_shop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController(CategoriesService catService)
        {
            _catService = catService;
        }

        private readonly CategoriesService _catService;

        // GET: api/Cat
        [HttpGet]
        public List<Categories> Get()
        {
            return _catService.GetAllCategory();

        }

        // GET: api/Cat/5
        [HttpGet("{id}")]
        public Categories GetById(int id)
        {
            var cat = _catService.GetCategoryById(id);
            return cat;
        }

        [HttpGet("/allCategory")]
        public List<Product> GetAllProductByCategory([FromBody] Product product)
        {
            var products = _catService.GetProductsByCategory(product.CategoryId);
                return products;
        }

        // POST: api/Cat
        [HttpPost]
        public IActionResult AddNewCategory([FromBody] Categories categories)
        {
            var category = _catService.AddCategories(categories.Name);
            if (category == false)
                return BadRequest("Bad request");
            else
                return Ok("Code 200, new category is added");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("/delete")]
        public IActionResult Delete(int id)
        {
            var category = _catService.DeleteCategoryById(id);
            if (category == false)
                return BadRequest("404");
            else
                return Ok("200");
        }
    }
}
