using KDP_EC.Api.Helpers;
using KDP_EC.Core.Interfaces.Account;
using KDP_EC.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KDP_EC.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUsersLogin _Iuser;
        public AccountController(JwtSettings jwtSettings, IUsersLogin Iuser)
        {
            _jwtSettings = jwtSettings;
            _Iuser = Iuser;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin user)
        {
            Guid? userId;

            if (!_Iuser.Login(user.Username, user.Password, out userId) || userId == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())  
        }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return Ok(new { token = jwt, userId });
        }

        [HttpGet("userInfo")]
        public IActionResult GetUserInfo(Guid userId)
        {
            var result = _Iuser.GetUsersInfo(userId);

            if (result == null || result.Count == 0)
            {
                return NotFound("No se encontraron usuarios con el ID proporcionado.");
            }

            return Ok(result);
        }

        [HttpPost("createUser")]

        public IActionResult CreateUser([FromBody] Users user)
        {
            if (user == null)
                return BadRequest("Los datos del usuario no pueden ser nulos.");
            bool isCreated = _Iuser.CreateUser(user.Id ,user.UserName, user.Password);

            if (isCreated)
            {
                return Ok(new { message = "Usuario creado exitosamente." });
            }
            else
            {
                return StatusCode(500, new { message = "Error al crear el usuario." });
            }
        }
    }


}
