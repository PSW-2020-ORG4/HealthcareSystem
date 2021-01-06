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
        private readonly IPatientService _patientService;
        private readonly IAdminService _adminService;
        public UserController(IPatientService patientService, IAdminService adminService)
        {
            _patientService = patientService;
            _adminService = adminService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserInfoDTO userInfoDTO)
        {
            /*var client = new RestClient("http://localhost:" + ServerConstants.PORT);
            var request = new RestRequest("/api/user", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(userInfoDTO);

            var response = client.Execute(request);

            var contentResult = new ContentResult();
            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            try
            {
                UserDTO userDTO = JsonConvert.DeserializeObject<UserDTO>(response.Content);
                if (userDTO.CanLogIn)
                    contentResult.Content = GenerateJWT(userDTO.Email, userDTO.Jmbg, userDTO.Type);
            }
            catch (Exception) { }

            return contentResult;*/
            try
            {
                string patientToken = TryToLoginPatient(userInfoDTO.Email, userInfoDTO.Password);
                string adminToken = TryToLoginAdmin(userInfoDTO.Email, userInfoDTO.Password);

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
