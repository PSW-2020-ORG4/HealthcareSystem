using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Model.Users;
using Backend.Service.SendingMail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientWebApp.Validators;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly PatientValidator _patientValidator;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
            _patientValidator = new PatientValidator();
        }

        [HttpPost("welcome")]
        public async Task<IActionResult> SendWelcomeMail([FromForm] WelcomeRequest request)
        {
            try
            {
                 _patientValidator.checkIfEmailValid(request.ToEmail);
                await _mailService.SendWelcomeEmailAsync(request);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }

            return Ok();
        }
    }
}
