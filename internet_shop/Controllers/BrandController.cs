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
    public class BrandController : Controller
    {
        private readonly BrandService _brandService;

        public BrandController(BrandService brandService)
        {
            _brandService = brandService;
        }
        // GET: api/Brand
        [HttpGet]
        public IEnumerable<Brand> Get()
        {
            return _brandService.GetAllBrand();
        }

        // GET: api/Brand/5
        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            Brand brand = _brandService.GetBrandById(id);
            if (brand == null)
            {
                return NotFound($"Unfortunately no brand found with id: {id}");
            }
            else
            {
                return Ok(brand);
            }
        }

        // POST: Brand/Create
        [HttpPost]
        public IActionResult Post([FromBody] Brand value)
        {
            var data = _brandService.AddBrand(value.Name);

            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        // GET: Brand/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Brand/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
