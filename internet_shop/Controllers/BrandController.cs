using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using shop.Models;
using shop.Services;

namespace shop.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("{id}", Name = "Get")]

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
            var data = _brandService.AddBrand(value.Name, value.Value);

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