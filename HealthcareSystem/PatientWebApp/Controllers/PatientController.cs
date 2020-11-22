using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Users;
using PatientWebApp.Adapters;
using PatientWebApp.DTOs;
using PatientWebApp.Validators;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IPatientCardService _patientCardService;
        private readonly PatientValidator _patientValidator;
        public PatientController(IPatientService patientService, IPatientCardService patientCardService)
        {
            _patientService = patientService;
            _patientCardService = patientCardService;
            _patientValidator = new PatientValidator();
        }

        /// <summary>
        /// /getting patient by jmbg
        /// </summary>
        /// <param name="jmbg">jmbg of the wanted patient</param>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        [HttpGet("{jmbg}")]
        public ActionResult GetPatientByJmbg(string jmbg)
        {
            try
            {
                Patient patient = _patientService.ViewProfile(jmbg);
                PatientCard patientCard = _patientCardService.ViewPatientCard(jmbg);
                return Ok(PatientMapper.PatientAndPatientCardToPatientDTO(patient, patientCard));
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        /// <summary>
        /// /adding new patient and patient card to database
        /// </summary>
        /// <param name="patientDTO">an object to be added to the database</param>
        /// <returns>if alright returns code 200(Ok), if not 400(bad request)</returns>
        [HttpPost]
        public ActionResult AddPatient(PatientDTO patientDTO)
        {
            try
            {
                _patientValidator.validatePatientFields(patientDTO);
                _patientService.RegisterPatient(PatientMapper.PatientDTOToPatient(patientDTO));
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
        [HttpPut("{jmbg}")]
        public ActionResult ActivatePatient(string jmbg)
        {
            try
            {
                _patientService.ActivatePatientStatus(jmbg);
                return Ok();
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }

        }
    }
}
