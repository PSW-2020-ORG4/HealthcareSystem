using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PatientService.DTOs;
using PatientWebApp.Auth;
using PatientWebApp.Controllers.Adapter;
using PatientWebApp.DTOs;
using PatientWebApp.Settings;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly ServiceSettings _serviceSettings;

        public ExaminationController(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }

        /// <summary>
        /// /getting searched examinations linked to a certain patient
        /// </summary>
        /// <param name="examinationSearchDTO">an object need be find in the database</param>
        /// <returns>if alright returns code 200(Ok), if not 400(not found)</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("documentation/advance-search")]
        public ActionResult AdvanceSearchExaminations(ExaminationSearchDTO examinationSearchDTO)
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;

            return RequestAdapter.SendRequestWithBody(_serviceSettings.PatientServiceUrl, "/api/patient/" + patientJmbg + "/examination/search", examinationSearchDTO);
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            /*var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            var contentResult = RequestAdapter.SendRequestWithoutBody(_serviceSettings.ScheduleServiceUrl, "/api/examination/" + id, Method.GET);
            var examinationDTO = (ScheduledExaminationDTO)JsonConvert.DeserializeObject(contentResult.Content);

            if (examinationDTO.PatientJmbg != patientJmbg)
                return StatusCode(403, "Patient tried to get someone else's examination");

            return contentResult;*/
            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.ScheduleServiceUrl, "/api/examination/" + id, Method.GET);
        }

        /// <summary>
        /// / updating examiantionStatus (property: ExaminationStatus) to CANCELED
        /// </summary>
        /// <param name="id">id of the object to be changed</param>
        /// <returns>if alright returns code 200(Ok), if not 400(bed request), if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost("cancel/{id}")]
        public ActionResult CancelExamination(int id)
        {
            /*var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            var contentResult = RequestAdapter.SendRequestWithoutBody(_serviceSettings.ScheduleServiceUrl, "/api/examination/" + id, Method.GET);
            var examinationDTO = (ScheduledExaminationDTO)JsonConvert.DeserializeObject(contentResult.Content);

            if (examinationDTO.PatientJmbg != patientJmbg)
                return StatusCode(403, "Patient tried to cancel someone else's examination");*/

            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.ScheduleServiceUrl, "/api/examination/" + id + "/cancel", Method.POST);
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("documentation")]
        public ActionResult GetFinishedExaminationsByPatient()
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;

            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.PatientServiceUrl, "/api/patient/" + patientJmbg + "/examination", Method.GET);
        }

        /// /getting canceled examinations linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if patientJmbg is null returns 400, if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("cancelled")]
        public ActionResult GetCanceledExaminationsByPatient()
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;

            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.ScheduleServiceUrl, "/api/examination/canceled/patient/" + patientJmbg, Method.GET);
        }

        /// <summary>
        /// /getting previous examinations linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if patientJmbg is null returns 400, if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("previous")]
        public ActionResult GetPreviousExaminationsByPatient()
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;

            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.ScheduleServiceUrl, "/api/examination/finished/patient/" + patientJmbg, Method.GET);
        }

        /// <summary>
        /// /getting following examinations linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if patientJmbg is null returns 400, if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("following")]
        public ActionResult GetFollowingExaminationsByPatient()
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;

            return RequestAdapter.SendRequestWithoutBody(_serviceSettings.ScheduleServiceUrl, "/api/examination/created/patient/" + patientJmbg, Method.GET);
        }

        /// <summary>
        /// /adding new examination to database
        /// </summary>
        /// <param name="examinationDTO">an object to be added to the database</param>
        /// <returns>if alright returns code 201(Created), if validation isn't successful return 400, if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost]
        public IActionResult AddExamination(ScheduleExaminationDTO examinationDTO)
        {
            var patientJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            examinationDTO.PatientJmbg = patientJmbg;

            return RequestAdapter.SendRequestWithBody(_serviceSettings.ScheduleServiceUrl, "/api/examination", examinationDTO);
        }

    }
}
