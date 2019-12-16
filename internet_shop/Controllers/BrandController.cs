using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using internet_shop.Models;
using internet_shop.Services;
using internet_shop.Dto;

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
        public IEnumerable<BrandDto> Get()
        {
            return _brandService.GetAllBrand();
        }

        // GET: api/Brand/5
        [HttpGet("{id}", Name = "Get")]

        public IActionResult Get(int id)
        {
            BrandDto brandDto = _brandService.GetBrandById(id);
            if (brandDto == null)
            {
                return NotFound($"Unfortunately no brand found with id: {id}");
            }
            else
            {
                return Ok(brandDto);
            }
        }

        // POST: api/Brand
        [HttpPost]
        public IActionResult Post([FromBody] Brand value)
        {
            BrandDto brand = _brandService.AddBrand(value.Name, value.Value);

            if (brand == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        // PUT: api/Brand/5
        [HttpPut]
        public IActionResult Put([FromBody] Brand value)
        {
            var data = _brandService.UpdateBrand(value);
            if (data.brandDto == null)
            {
                return BadRequest(data.exception.Message);
            }
            else
            {
                return Ok(data);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _brandService.DeleteBrandById(id);
            if (data.exception != null)
            {
                return BadRequest(data.exception.Message);
            }
            else
            {
                return Ok(data.result);
            }
        }
    }
}