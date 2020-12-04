using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service.ExaminationAndPatientCard;
using Backend.Service.SearchSpecification;
using Backend.Service.SearchSpecification.ExaminationSearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.PerformingExamination;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;
using PatientWebApp.Validators;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly IExaminationService _examinationService;
        private readonly ExaminationValidator _examinationValidator;


        public ExaminationController(IExaminationService examinationService)
        {
            _examinationService = examinationService;
            _examinationValidator = new ExaminationValidator(_examinationService);

        }
        /// <summary>
        /// /getting examinations linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        [HttpGet("by-patient/{patientJmbg}")]
        public ActionResult GetExaminationsByPatient(string patientJmbg)
        {
            try
            {
                List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
                _examinationService.GetExaminationsByPatient(patientJmbg).ForEach(examination => examinationDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(examination)));
                return Ok(examinationDTOs);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        /// <summary>
        /// /getting searched examinations linked to a certain patient
        /// </summary>
        /// <param name="examinationSearchDTO">an object need be find in the database</param>
        /// <returns>if alright returns code 200(Ok), if not 400(not found)</returns>
        [HttpPost("advance-search")]
        public ActionResult AdvanceSearchExaminations(ExaminationSearchDTO examinationSearchDTO)
        {
            try
            {
                List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
                _examinationService.AdvancedSearch(examinationSearchDTO).ForEach(examination => examinationDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(examination)));
                return Ok(examinationDTOs);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }

        }

        /// <summary>
        /// / updating examiantionStatus (property: ExaminationStatus) to CANCELED
        /// </summary>
        /// <param name="id">id of the object to be changed</param>
        /// <returns>if alright returns code 200(Ok), if not 400(bed request), if connection lost returns 500</returns>
        [HttpPut("cancel/{id}")]
        public ActionResult CancelExamination(int id)
        {
            try
            {
                _examinationValidator.CheckIfExaminationCanBeCanceled(id);
                _examinationService.CancelExamination(id);
                return Ok();
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (DatabaseException exception)
            {
                return StatusCode(500, exception.Message);
            }
            
        }

        /// /getting canceled examinations linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if patientJmbg is null returns 400, if connection lost returns 500</returns>
        [HttpGet("cancelled/{patientJmbg}")]
        public ActionResult GetCanceledExaminationsByPatient(string patientJmbg)
        {
            try
            {
                List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
                _examinationService.GetCanceledExaminationsByPatient(patientJmbg).ForEach(examination => examinationDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(examination)));
                return Ok(examinationDTOs);
            }
            catch (NullReferenceException)
            {
                return BadRequest("Patient's jmbg cannot be null.");
            }
            catch (DatabaseException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// /getting previous examinations linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if patientJmbg is null returns 400, if connection lost returns 500</returns>
        [HttpGet("previous/{patientJmbg}")]
        public ActionResult GetPreviousExaminationsByPatient(string patientJmbg)
        {
            try
            {
                List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
                _examinationService.GetPreviousExaminationsByPatient(patientJmbg).ForEach(examination => examinationDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(examination)));
                return Ok(examinationDTOs);
            }
            catch (NullReferenceException)
            {
                return BadRequest("Patient's jmbg cannot be null.");
            }
            catch (DatabaseException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// /getting following examinations linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if patientJmbg is null returns 400, if connection lost returns 500</returns>
        [HttpGet("following/{patientJmbg}")]
        public ActionResult GetFollowingExaminationsByPatient(string patientJmbg)
        {
            try
            {
                List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
                _examinationService.GetFollowingExaminationsByPatient(patientJmbg).ForEach(examination => examinationDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(examination)));
                return Ok(examinationDTOs);
            }
            catch (NullReferenceException)
            {
                return BadRequest("Patient's jmbg cannot be null.");
            }
            catch (DatabaseException e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet("{id}")]
        public ActionResult GetExaminationById(int id)
        {
            try
            {
                ExaminationDTO examinationDTO = new ExaminationDTO();
                examinationDTO = ExaminationMapper.ExaminationToExaminationDTO(_examinationService.GetExaminationById(id));
                return Ok(examinationDTO);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (DatabaseException e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
