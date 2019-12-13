using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using internet_shop.Models;
using internet_shop.Services;

namespace internet_shop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly OrderService orderService;
        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(int id) => Json(await orderService.GetAsync(id));

        [HttpGet]
        public async Task<IActionResult> Get() => Json(await orderService.GetAsync());

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] OrderModel orderModel)
             => Json(await orderService.AddAsync(orderModel));
    }
}
