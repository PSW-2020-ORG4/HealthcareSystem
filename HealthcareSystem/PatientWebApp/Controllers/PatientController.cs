using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Backend.Model.Exceptions;
using Backend.Model.Users;
using Backend.Service;
using Backend.Service.Encryption;
using Backend.Service.SendingMail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Users;
using PatientWebApp.Adapters;
using PatientWebApp.Constants;
using PatientWebApp.DTOs;
using PatientWebApp.Validators;
using RestSharp;

namespace PatientWebApp.Controllers
{
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

        public PatientController(IPatientService patientService, IPatientCardService patientCardService, IWebHostEnvironment webHostEnvironment, IMailService mailService)
        {
            _patientService = patientService;
            _patientCardService = patientCardService;
            _mailService = mailService;
            _patientValidator = new PatientValidator();
            _webHostEnvironment = webHostEnvironment;
            _encryptionService = new EncryptionService();
        }

        /// <summary>
        /// /getting patient by jmbg
        /// </summary>
        /// <param name="jmbg">jmbg of the wanted patient</param>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        [HttpGet("{jmbg}")]
        public IActionResult GetPatientByJmbg(string jmbg)
        {
            var client = new RestClient("http://localhost:" + ServerConstants.PORT);
            var request = new RestRequest("/api/patient/" + jmbg);
            var response = client.Execute(request);
            return StatusCode((int)response.StatusCode, response.Content);
        }

        /// <summary>
        /// /adding new patient and patient card to database
        /// </summary>
        /// <param name="patientDTO">an object to be added to the database</param>
        /// <returns>if alright returns code 200(Ok), if not 400(bad request)</returns>
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
        [HttpPut("activate/{jmbg}")]
        public ActionResult ActivatePatient(string jmbg)
        {
            try
            {
                string decryptedJmbg = _encryptionService.DecryptString(jmbg);
                _patientService.ActivatePatientStatus(decryptedJmbg);
                return Ok();
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }

        }
		
        /// /upload patient image in memory
        /// </summary>
        /// <param name="file">uploaded file ie image</param>
        /// <param name="patientJmbg">jmbg of patient who uploads file</param>
        /// <returns>if alright makes redirection to new action, if not stay at current page</returns>
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
        [HttpGet("malicious")]
        public IActionResult GetMaliciousPatients()
        {
            var client = new RestClient("http://localhost:" + ServerConstants.PORT);
            var request = new RestRequest("/api/patient/malicious");
            var response = client.Execute(request);
            return StatusCode((int)response.StatusCode, response.Content);
        }

        [HttpGet("{jmbg}/canceled-examinations")]
        public IActionResult GetNumberOfCanceledExaminations(string jmbg)
        {
            try
            {
                int number = _patientService.GetNumberOfCanceledExaminations(jmbg);
                return Ok(number);
            }
            catch (DatabaseException exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPost("{jmbg}/block")]
        public ActionResult BlockPatient(string jmbg)
        {
            var client = new RestClient("http://localhost:" + ServerConstants.PORT);
            var request = new RestRequest("/api/patient/"+jmbg+"/block");
            var response = client.Execute(request);
            return StatusCode((int)response.StatusCode, response.Content);

        }
    }
}
