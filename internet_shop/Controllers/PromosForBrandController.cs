using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using internet_shop.Models;
using internet_shop.Services;

namespace internet_shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromosForBrandController : Controller
    {
        private readonly PromosForBrandService _promosForBrandService;

        public PromosForBrandController(PromosForBrandService promosForBrandService)
        {
            _promosForBrandService = promosForBrandService;
        }
        // GET: promosForBrandModel
        [HttpGet]
        public IEnumerable<PromosForBrandModel> Get()
        {
            return _promosForBrandService.GetAllPromosForBrand();
        }

        // GET: PromosForBrandModel/5
        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            PromosForBrandModel promos = _promosForBrandService.GetPromoForBrandById(id);
            if (promos == null)
            {
                return NotFound($"No promosForBrandModel found with id: {id}");
            }
            else
            {
                return Ok(promos);
            }
        }


        // POST: PromosForBrandModel/Create
        [HttpPost]
        public IActionResult Post([FromBody] PromosForBrandModel value)
        {
            var data = _promosForBrandService.AddPromoForBrand(value.Id, value.Name, value.UniversalId, value.Enabled); //было value.Value поменял на Id

            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        // DELETE: PromosForBrandModel/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _promosForBrandService.DeletePromoForBrandById(id);
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
        public IActionResult Put([FromBody] PromosForBrandModel value)
        {
            var data = _promosForBrandService.UpdatepromoForBrand(value);
            if (data.promosForBrand == null)
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