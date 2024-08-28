using JwtModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string privateKey;
        private readonly string symmetricKey;
        private readonly int timeOut;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            privateKey = _configuration.GetSection("PrivateKey").Value;
            symmetricKey = _configuration.GetSection("SymmetricKey").Value;
            timeOut = int.Parse(_configuration.GetSection("TimeOut").Value);
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserLogin user)
        {
            // Validate the user credentials (this is just a dummy check, replace with your logic)
            if (user.key == symmetricKey)
            {
                var token = GenerateJwtToken(user.key);
                return Ok(new { token });
            }

            return Unauthorized();
        }
        private string GenerateJwtToken(string userKey)
        {
            var key = Encoding.UTF8.GetBytes(privateKey);
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userKey)
                }),
                Expires = DateTime.UtcNow.AddHours(timeOut),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
        }

    }
}
