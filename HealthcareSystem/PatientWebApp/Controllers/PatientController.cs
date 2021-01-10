using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Auth;
using PatientWebApp.DTOs;
using PatientWebApp.Settings;
using RestSharp;
using System.IO;
using System.Net;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly EncryptionService _encryptionService;
        private readonly ServiceSettings _serviceSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatientController(IWebHostEnvironment webHostEnvironment,
                                 IOptions<ServiceSettings> serviceSettings,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _encryptionService = new EncryptionService();
            _serviceSettings = serviceSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// /getting patient by jmbg
        /// </summary>
        /// <param name="jmbg">jmbg of the wanted patient</param>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet]
        public IActionResult GetPatientByJmbg()
        {
            var jmbg = HttpContext.User.FindFirst("Jmbg").Value;
            var client = new RestClient(_serviceSettings.UserServiceUrl);
            var request = new RestRequest("/api/patient/" + jmbg);
            var response = client.Execute(request);
            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("medical-info")]
        public IActionResult GetPatientMedicalInfo()
        {
            var jmbg = HttpContext.User.FindFirst("Jmbg").Value;
            var client = new RestClient(_serviceSettings.PatientServiceUrl);
            var request = new RestRequest("/api/patient/" + jmbg + "/medical-info");
            var response = client.Execute(request);
            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }

        /// <summary>
        /// /adding new patient and patient card to database
        /// </summary>
        /// <param name="patientDTO">an object to be added to the database</param>
        /// <returns>if alright returns code 200(Ok), if not 400(bad request)</returns>
        /// 
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddPatient(PatientDTO patientDTO)
        {
            var userServiceClient = new RestClient(_serviceSettings.UserServiceUrl);
            var userServiceRequest = new RestRequest("/api/patient/", Method.POST);
            userServiceRequest.AddJsonBody(new
            {
                Name = patientDTO.Name,
                Surname = patientDTO.Surname,
                Jmbg = patientDTO.Jmbg,
                Gender = patientDTO.Gender,
                DateOfBirth = patientDTO.DateOfBirth,
                Phone = patientDTO.Phone,
                CityId = patientDTO.CityZipCode,
                HomeAddress = patientDTO.HomeAddress,
                Email = patientDTO.Email,
                Password = patientDTO.Password,
                ImageName = "/images/Blank-profile.png"
            });
            var response = userServiceClient.Execute(userServiceRequest);

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                return new ContentResult()
                {
                    StatusCode = (int)response.StatusCode,
                    Content = response.Content,
                    ContentType = response.ContentType
                };
            }

            var patientServiceClient = new RestClient(_serviceSettings.PatientServiceUrl);
            var patientServiceRequest = new RestRequest("/api/patient/" + patientDTO.Jmbg + "/medical-info",
                                                        Method.PUT);
            patientServiceRequest.AddJsonBody(new
            {
                BloodType = patientDTO.BloodType,
                RhFactor = patientDTO.RhFactor,
                Allergies = patientDTO.Allergies,
                MedicalHistory = patientDTO.MedicalHistory,
                InsuranceNumber = patientDTO.Lbo
            });
            patientServiceClient.Execute(patientServiceRequest);

            string encryptedJmbg = _encryptionService.EncryptString(patientDTO.Jmbg);
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
            var notificationServiceClient = new RestClient(_serviceSettings.NotificationServiceUrl);
            var notificationServiceRequest = new RestRequest("/api/notify/activation", Method.POST);
            notificationServiceRequest.AddJsonBody(new
            {
                Email = patientDTO.Email,
                Name = patientDTO.Name,
                ActivationLink = $"http://{host}/html/activate_account.html?id={encryptedJmbg}"
            });
            notificationServiceClient.Execute(notificationServiceRequest);

            return NoContent();
        }

        /// <summary>
        /// /activate patient (property: IsActive) to true
        /// </summary>
        /// <param name="id">id of the object to be changed</param>
        /// <returns>if alright returns code 200(Ok), if not 400(bed request)</returns>
        /// 
        [AllowAnonymous]
        [HttpPost("{jmbg}/activate")]
        public ActionResult ActivatePatient(string jmbg)
        {
            string decryptedJmbg = _encryptionService.DecryptString(jmbg);
            var client = new RestClient(_serviceSettings.UserServiceUrl);
            var request = new RestRequest("/api/patient/" + decryptedJmbg + "/activate", Method.POST);
            var response = client.Execute(request);
            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }

        /// /upload patient image in memory
        /// </summary>
        /// <param name="file">uploaded file ie image</param>
        /// <param name="patientJmbg">jmbg of patient who uploads file</param>
        /// <returns>if alright makes redirection to new action, if not stay at current page</returns>
        /// 
        [AllowAnonymous]
        [HttpPost]
        [Route("upload")]
        public IActionResult UploadImage([FromForm] IFormFile file, [FromQuery] string patientJmbg)
        {
            if (file == null || file.Length == 0)
            {
                return RedirectPermanent("/html/patient_registration.html");
            }
            string directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");
            string imagePath = Path.Combine(directoryPath, file.FileName);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            using (FileStream fileStream = System.IO.File.Create(imagePath))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            return RedirectToAction("ChangeImagePathForPatent", new { jmbg = patientJmbg, name = file.FileName });
        }

        /// <summary>
        /// /change patient image name for certain patient in database
        /// </summary>
        /// <param name="jmbg">jmbg of patient</param>
        /// <param name="name">image name</param>
        /// <returns>if alright makes redirection to patient home page, if not return 404(Not found patient)</returns>
        /// 
        [AllowAnonymous]
        [HttpGet("{jmbg}/{name}")]
        public IActionResult ChangeImagePathForPatent(string jmbg, string name)
        {
            var userServiceClient = new RestClient(_serviceSettings.UserServiceUrl);
            var userServiceRequest = new RestRequest("/api/patient/" + jmbg + "/image", Method.PUT);
            userServiceRequest.AddJsonBody("/Uploads/" + name);
            userServiceClient.Execute(userServiceRequest);
            return RedirectPermanent("/html/patients_home_page.html");
        }

        /// <summary>
        /// /getting malicious patients(who canceled examinations 3 or more times in the past month)
        /// </summary>
        /// <returns>list of patients</returns>
        /// 
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("malicious")]
        public IActionResult GetMaliciousPatients()
        {
            var client = new RestClient(_serviceSettings.UserServiceUrl);
            var request = new RestRequest("/api/patient/malicious");
            var response = client.Execute(request);
            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("{jmbg}/block")]
        public ActionResult BlockPatient(string jmbg)
        {
            var client = new RestClient(_serviceSettings.UserServiceUrl);
            var request = new RestRequest("/api/patient/" + jmbg + "/block", Method.POST);
            var response = client.Execute(request);
            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }
    }
}
