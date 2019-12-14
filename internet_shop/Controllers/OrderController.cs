using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using internet_shop.Models;
using internet_shop.Services;

namespace internet_shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly OrderService orderService;
        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }
        [Route("/{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id) => Json(await orderService.GetAsync(id));

        //  [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromQuery] OrderModel orderModel)
             => Json(await orderService.AddAsync(orderModel));



        //// GET: api/Promos/5
        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    Order order = orderService.GetOrderById(id);
        //    if (order == null)
        //    {
        //        return NotFound($"No promo found with id: {id}");
        //    }
        //    else
        //    {
        //        return Ok(order);
        //    }
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var data = orderService.DeleteOrderById(id);
        //    if (data.exception != null)
        //    {
        //        return BadRequest(data.exception.Message);
        //    }
        //    else
        //    {
        //        return Ok(data.result);
        //    }
        //}

    }
}
