using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using internet_shop.Models;
using internet_shop.Services;

namespace internet_shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromosController : Controller
    {
        private readonly PromosService _promosService;

        public PromosController(PromosService promosService)
        {
            _promosService = promosService;
        }
        // GET: Promos
        [HttpGet]
        public IEnumerable<Promos> Get()
        {
            return _promosService.GetAllPromos();
        }

        // GET: Promos/5
        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            Promos promos = _promosService.GetPromoById(id);
            if (promos == null)
            {
                return NotFound($"No promo found with id: {id}");
            }
            else
            {
                return Ok(promos);
            }
        }


        // POST: Promos/Create
        [HttpPost("/Promos/Create")]
        public IActionResult Post([FromBody] Promos value)
        {
            var data = _promosService.AddPromo(value.Name, value.Value, value.CategoryId, value.BrandId,value.ProductId,value.IsEnabled );

            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        // DELETE: Promos/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _promosService.DeletePromoById(id);
            if (data.exception != null)
            {
                return BadRequest(data.exception.Message);
            }
            else
            {
                return Ok(data.result);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Promos value)
        {
            var data = _promosService.UpdatePromos(value);
            if (data.promos == null)
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