using Microsoft.AspNetCore.Mvc;

using internet_shop.Services;
using internet_shop.Models;

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

        // GET: Product
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet("{id}")]//Product/1
        public IActionResult GetById(int id)
        {
            var product = _productService.GetProduct(id);
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
        public IActionResult UpdateProductToPromos()
        {
            var product = _productService.UpdateProduct();
            if (product == false)
                return BadRequest("400");
            else
                return Ok("200");

        }

        [HttpPost("/add")]//add?Name=Greta&&Description=SD
        public IActionResult AddById([FromBody] Product value)
        {
            var product = _productService.AddNewProduct(value.Name, value.Description, value.BrandId,value.CategoryId,value.Price, value.Price);
            if (product == false)
                return NotFound("Bad Request");
            else
                return Ok("New produc is added");
            //[FromBody] string name, [FromBody] string description, [FromBody] int price
            //[FromQuery] string name, [FromQuery] string description, [FromQuery] int price
        }
    }
}
