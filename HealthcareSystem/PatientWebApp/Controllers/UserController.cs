using System;
using System.Text;
using Backend.Model.Exceptions;
using Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Users;
using PatientWebApp.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IPatientService _patientService;
        private readonly IAdminService _adminService;
        public UserController(IConfiguration config, IPatientService patientService, IAdminService adminService)
        {
            _config = config;
            _patientService = patientService;
            _adminService = adminService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentialsDTO userCredentialsDTO)
        {
            try
            {
                string patientToken = TryToLoginPatient(userCredentialsDTO.Username, userCredentialsDTO.Password);
                string adminToken = TryToLoginAdmin(userCredentialsDTO.Username, userCredentialsDTO.Password);

                if (patientToken == null && adminToken == null)
                {
                    return Unauthorized();
                }
                else if (patientToken != null)
                {
                    return Ok(patientToken);
                }
                else
                {
                    return Ok(adminToken);
                }
            }
            catch (BadRequestException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (DatabaseException exception)
            {
                return StatusCode(500, exception.Message);
            }
            
        }

       [AllowAnonymous]
       [HttpGet("logged")]
       public IActionResult GetLoggedUser([FromBody] string token)
       {
           var key = Encoding.ASCII.GetBytes(token);
           var handler = new JwtSecurityTokenHandler();
           var validations = new TokenValidationParameters
           {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(key),
               ValidateIssuer = false,
               ValidateAudience = false
           };
           var claims = handler.ValidateToken(token, validations, out var tokenSecure);

           string username = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
           string role = claims.Claims.First(c => c.Type == ClaimTypes.Role).Value;
           string jmbg = claims.Claims.First(c => c.Type == "Jmbg").Value;

           return Ok(new LoggedUserDTO(username, jmbg, role));
       }

        private string TryToLoginPatient(string username, string password)
        {
            Patient patient;
            try
            {
                patient = _patientService.GetPatientByUsernameAndPassword(username, password);
                string token = GenerateJWT(patient.Username, patient.Jmbg, UserRoles.Patient);
                return token;
            }
            catch (NotFoundException)
            {
                return null;
            }
        }
        private string TryToLoginAdmin(string username, string password)
        {
            Admin admin;
            try
            {
                admin = _adminService.GetAdminByUsernameAndPassword(username, password);
                string token = GenerateJWT(admin.Username, admin.Jmbg, UserRoles.Admin);
                return token;
            }
            catch (NotFoundException)
            {
                return null;
            }
        }

        private string GenerateJWT(string username, string jmbg, string role)
        {
            var tokenKey = _config.GetValue<string>("TokenKey");
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, username), new Claim("Jmbg",jmbg) , new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
