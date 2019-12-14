using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using internet_shop.Models;
using internet_shop.Services;

namespace internet_shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromosForCategoriesController : Controller
    {
        private readonly PromosForCategoriesService _promosForCategoriesService;

        public PromosForCategoriesController(PromosForCategoriesService promosForCategoriesService)
        {
            _promosForCategoriesService = promosForCategoriesService;
        }
        // GET: PromosForCategoriesModel
        [HttpGet]
        public IEnumerable<PromosForCategoriesModel> Get()
        {
            return _promosForCategoriesService.GetAllPromosForCategories();
        }

        // GET: PromosForCategoriesModel/5
        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            PromosForCategoriesModel promos = _promosForCategoriesService.GetPromoForCategoriesById(id);
            if (promos == null)
            {
                return NotFound($"No PromosForCategoriesModel found with id: {id}");
            }
            else
            {
                return Ok(promos);
            }
        }


        // POST: PromosForCategoriesModel/Create
        [HttpPost]
        public IActionResult Post([FromBody] PromosForCategoriesModel value)
        {
            var data = _promosForCategoriesService.AddPromoForCategories(value.Id, value.Name, value.UniversalId); //было value.Value поменял на Id

            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        // DELETE: PromosForCategoriesModel/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _promosForCategoriesService.DeletePromoForCategoriesById(id);
            if (data.exception != null)
            {
                return BadRequest(data.exception.Message);
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] PromosForCategoriesModel value)
        {
            var data = _promosForCategoriesService.UpdatepromoForCategories(value);
            if (data.promosForCategories == null)
            {
                return BadRequest(data.exception.Message);
            }
            else
            {
                return Ok(data);
            }
        }
    }
}