using Eticaret.Business.Dtos;
using Eticaret.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Eticaret.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        // GET: api/<UsersController>
        [Authorize]
        [HttpPost("GetUserByLogin")]
        public ActionResult GetUserByLogin([FromBody] LoginDto loginDto)
        {
            var user = _userService.GetUserByLogin(loginDto.EmailAddress, loginDto.Password);

            if (user != null)
                return Ok(user);
            else
                return NotFound();
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public ActionResult Authenticate([FromBody] LoginDto loginDto)
        {
            var user = _userService.GetUserByLogin(loginDto.EmailAddress, loginDto.Password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name ?? ""),
                    new Claim(ClaimTypes.GivenName, user.Name ?? ""),
                    new Claim(ClaimTypes.Surname, user.Surname ?? ""),
                    new Claim(ClaimTypes.Email, user.EmailAddress)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:SigningKey"]));

                var tokenOptions = new JwtSecurityToken(
                    issuer: _configuration["Auth:Issuer"],
                    audience: _configuration["Auth:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

                var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new
                {
                    AccessToken = accessToken
                });
            }
            else
                return NotFound();
        }
    }
}
