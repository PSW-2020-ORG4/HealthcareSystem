﻿using Backend.Model.Exceptions;
using Backend.Model.Users;
using Backend.Service;
using Backend.Service.Encryption;
using Backend.Service.SendingMail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Adapters;
using PatientWebApp.Auth;
using PatientWebApp.Controllers.Adapter;
using PatientWebApp.DTOs;
using PatientWebApp.Settings;
using PatientWebApp.Validators;
using RestSharp;
using System.IO;
using System.Threading.Tasks;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IPatientCardService _patientCardService;
        private readonly IMailService _mailService;
        private readonly PatientValidator _patientValidator;
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly EncryptionService _encryptionService;
        private readonly ServiceSettings _serviceSettings;

        public PatientController(IPatientService patientService,
                                 IPatientCardService patientCardService,
                                 IWebHostEnvironment webHostEnvironment,
                                 IMailService mailService,
                                 IOptions<ServiceSettings> serviceSettings)
        {
            _patientService = patientService;
            _patientCardService = patientCardService;
            _mailService = mailService;
            _patientValidator = new PatientValidator();
            _webHostEnvironment = webHostEnvironment;
            _encryptionService = new EncryptionService();
            _serviceSettings = serviceSettings.Value;
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
            return RequestAdapter.SendGetRequest(_serviceSettings.UserServiceUrl, "/api/patient/" + jmbg);
        }

        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("medical-info")]
        public IActionResult GetPatientMedicalInfo()
        {
            var jmbg = HttpContext.User.FindFirst("Jmbg").Value;
            return RequestAdapter.SendGetRequest(_serviceSettings.PatientServiceUrl, "/api/patient/" + jmbg + "/medical-info");
        }

        /// <summary>
        /// /adding new patient and patient card to database
        /// </summary>
        /// <param name="patientDTO">an object to be added to the database</param>
        /// <returns>if alright returns code 200(Ok), if not 400(bad request)</returns>
        /// 
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddPatient(PatientDTO patientDTO)
        {
            try
            {
                WelcomeRequest request = new WelcomeRequest(patientDTO.Email, patientDTO.Name, patientDTO.Jmbg);
                _patientValidator.validatePatientFields(patientDTO);
                _patientService.RegisterPatient(PatientMapper.PatientDTOToPatient(patientDTO));
                await _mailService.SendWelcomeEmailAsync(request);
                _patientCardService.AddPatientCard(PatientMapper.PatientDTOToPatientCard(patientDTO));

            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (BadRequestException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (DatabaseException exception)
            {
                return BadRequest(exception.Message);
            }
            return Ok();
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
            string directoryPath = _webHostEnvironment.WebRootPath + "\\Uploads\\";
            string imagePath = directoryPath + file.FileName;
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
            try
            {
                _patientService.SavePatientImageName(jmbg, name);

            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
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
            return RequestAdapter.SendGetRequest(_serviceSettings.UserServiceUrl, "/api/patient/malicious");
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
