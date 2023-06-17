using Eticaret.Business.Dtos;
using Eticaret.Business.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eticaret.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [HttpPost("GetUserByLogin")]
        public ActionResult GetUserByLogin([FromBody] LoginDto loginDto)
        {
            var user = _userService.GetUserByLogin(loginDto.EmailAddress, loginDto.Password);

            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }
    }
}
