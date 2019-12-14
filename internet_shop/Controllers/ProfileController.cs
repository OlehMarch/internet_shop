using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using internet_shop.Models;
using internet_shop.Services;

namespace internet_shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : Controller
    {
        public ProfileController(UserService userService)
        {
            _userService = userService;
        }

        private readonly UserService _userService;

        [HttpGet]
        public IEnumerable<Profile> Get()
        {
            return _userService.GetAllProfiles();
        }

        [HttpGet("{id}")]

        public IActionResult Get(int id)
        {
            Profile profile = _userService.GetProfileById(id);
            if (profile == null)
            {
                return NotFound($"No profile found with id: {id}");
            }
            else
            {
                return Ok(profile);
            }
        }
        [HttpPost("registration")]
        public IActionResult Registration([FromBody]Profile value)
        {
            var data = _userService.AddUser(value.FirstName, value.LastName, value.Email, value.Address, value.Username, value.Password);
            if (data == null)
                return BadRequest(new { message = "Username is created" });

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = _userService.DeleteProfileById(id);
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
        public IActionResult Put([FromBody] Profile value)
        {
            var data = _userService.Updateprofile(value);
            if (data.profile == null)
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