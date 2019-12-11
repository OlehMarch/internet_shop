using Microsoft.AspNetCore.Mvc;

using internet_shop.Services;

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

        [HttpGet("{id}", Name = "Get")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetProduct(id);
            if (product == null)
                return NotFound("Bad request");
            else
                return Ok(product);
        }

        [HttpGet]
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

        [HttpPost("/add")]
        public IActionResult AddById([FromQuery] string name, [FromQuery] string description, [FromQuery] int price)
        {
            var product = _productService.AddNewProduct(name, description, price);
            if (product == false)
                return NotFound("Bad Request");
            else
                return Ok("New produc is added");
            //[FromBody] string name, [FromBody] string description, [FromBody] int price
            //[FromQuery] string name, [FromQuery] string description, [FromQuery] int price
        }
    }
}
