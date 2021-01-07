using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientWebApp.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PatientWebApp.Constants;
using RestSharp;
using Newtonsoft.Json;
using Backend.Service;
using Backend.Model.Exceptions;
using Model.Users;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserInfoDTO userInfoDTO)
        {
            var client = new RestClient("http://localhost:" + ServerConstants.PORT);
            var request = new RestRequest("/api/user", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(userInfoDTO);
            var response = client.Execute(request);

            try
            {
                UserDTO userDTO = JsonConvert.DeserializeObject<UserDTO>(response.Content);
                if (userDTO.CanLogIn)
                {
                    var token = GenerateJWT(userDTO.Email, userDTO.Jmbg, userDTO.Type);
                    return Ok(token);
                }
            }
            catch (Exception) { }

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
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
