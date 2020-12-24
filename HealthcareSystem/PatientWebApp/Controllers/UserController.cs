using Backend.Model.Exceptions;
using Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Users;
using PatientWebApp.DTOs;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IJWTAuthenticationManager _jWTAuthenticationManager;
        private readonly IPatientService _patientService;
        private readonly IAdminService _adminService;
        public UserController(IJWTAuthenticationManager jWTAuthenticationManager, IPatientService patientService, IAdminService adminService)
        {
            _jWTAuthenticationManager = jWTAuthenticationManager;
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
                string token = _jWTAuthenticationManager.Authenticate(patient.Username, patient.Jmbg, UserRoles.Patient);
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
                string token = _jWTAuthenticationManager.Authenticate(admin.Username, admin.Jmbg, UserRoles.Admin);
                return token;
            }
            catch (NotFoundException)
            {
                return null;
            }
        }
    }
}
