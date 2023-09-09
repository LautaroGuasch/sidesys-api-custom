using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using STE.Models;
using STE.Service;
using System.Xml.Linq;

namespace STE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserModel>> GetUser(string userId)
        {
            var response = await _userService.GetUserById(userId);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<object>> CreateUser([FromBody] UserModel model)
        {
            var response = await _userService.CreateUser(model);

            if (response != null)
            {
                UserCreatedOutput userCreated = JsonConvert.DeserializeObject<UserCreatedOutput>(response.ToString());
                return StatusCode(201, $"Objeto creado correctamente con id: {userCreated.Name}");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{userId}")]

        public async Task<ActionResult<object>> DeleteUser(string userId)
        {
            var response = await _userService.DeleteUser(userId);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{userId}")]

        public async Task<ActionResult<object>> PutUser([FromBody] UserModel model, string userId)
        {
            var response = await _userService.UpdateUser(model, userId);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
