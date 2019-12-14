using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using internet_shop.Models;
using internet_shop.Services;

namespace internet_shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromosForProductController : Controller
    {
        private readonly PromosForProductService _promosForProducService;

        public PromosForProductController(PromosForProductService promosForProducService)
        {
            _promosForProducService = promosForProducService;
        }
        // GET: PromosForProductModel
        [HttpGet]
        public IEnumerable<PromosForProductModel> Get()
        {
            return _promosForProducService.GetAllPromosForProduct();
        }

        // GET: PromosForProductModel/5
        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            PromosForProductModel promos = _promosForProducService.GetPromoForProductById(id);
            if (promos == null)
            {
                return NotFound($"No PromosForProductModel found with id: {id}");
            }
            else
            {
                return Ok(promos);
            }
        }


        // POST: PromosForProductModel/Create
        [HttpPost]
        public IActionResult Post([FromBody] PromosForProductModel value)
        {
            var data = _promosForProducService.AddPromoForProduct(value.Id, value.Name, value.UniversalId , value.Enabled); //было value.Value поменял на Id

            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        // DELETE: PromosForProductModel/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _promosForProducService.DeletePromoForProductById(id);
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
        public IActionResult Put([FromBody] PromosForProductModel value)
        {
            var data = _promosForProducService.UpdatepromoForProduct(value);
            if (data.promosForProduct == null)
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