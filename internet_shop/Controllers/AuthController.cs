using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using internet_shop.Models;
using internet_shop.Services;

namespace internet_shop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        private readonly UserService _userService;

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
    }
}
