using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using internet_shop.Services;
using internet_shop.Models;

namespace internet_shop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private CategoriesService _catService;
        public CategoriesController(CategoriesService catService)
        {
            _catService = catService;
        }
        // GET: api/Cat
        [HttpGet]
        public List<Categories> Get()
        {
            return _catService.GetAllCategory();
            
        }

        // GET: api/Cat/5
        [HttpGet("{id}")]
        public Categories Get(int id)
        {
            var cat = _catService.GetCategoryById(id);
            return cat;
        }

        // POST: api/Cat
        [HttpPost]
        public IActionResult AddNewCategory([FromBody] Categories categories)
        {
            var category = _catService.AddCategories(categories.Name,categories.Value);
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
