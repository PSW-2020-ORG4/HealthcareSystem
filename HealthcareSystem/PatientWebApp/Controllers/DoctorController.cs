using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Auth;
using PatientWebApp.Controllers.Adapter;
using PatientWebApp.Settings;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ServiceSettings _serviceSettings;

        public DoctorController(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }

        [Authorize(Roles = UserRoles.Patient + "," + UserRoles.Admin)]
        [HttpGet]
        public ActionResult GetAllDoctors()
        {
            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.UserServiceUrl, "/api/doctor", Method.GET);
        }

        /// <summary>
        /// /getting doctorSpecialty by idSpecilaty
        /// </summary>
        /// <param name="id">id of the wanted object</param>
        /// <returns>if alright returns code 200(Ok), if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("doctor-specialty/{id}")]
        public IActionResult GetSpecialistDoctorsBySpecialtyId(int id)
        {
            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.UserServiceUrl, "/api/doctor/specialty/" + id, Method.GET);
        }
    }
}
