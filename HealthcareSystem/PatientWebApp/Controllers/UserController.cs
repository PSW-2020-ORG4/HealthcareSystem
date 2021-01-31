using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PatientWebApp.Controllers.Adapter;
using PatientWebApp.DTOs;
using PatientWebApp.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ServiceSettings _serviceSettings;

        public UserController(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserInfoDTO userInfoDTO)
        {
            var contentResult = RequestAdapter.SendRequestWithBody(_serviceSettings.UserServiceUrl, "/api/user", userInfoDTO);

            try
            {
                UserDTO userDTO = JsonConvert.DeserializeObject<UserDTO>(contentResult.Content);
                if (userDTO.CanLogIn)
                {
                    var token = GenerateJWT(userDTO.Email, userDTO.Jmbg, userDTO.Type);
                    return Ok(token);
                }
            }
            catch (Exception) { }

            if ((int)contentResult.StatusCode == 500)
                return Problem("An internal error occured.");
            else
                return Unauthorized("Credentials not valid.");
        }

        private string GenerateJWT(string email, string jmbg, string type)
        {
            var tokenKey = "This is my private key";
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, email), new Claim("Jmbg",jmbg) , new Claim(ClaimTypes.Role, type)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
