using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Model.Exceptions;
using Backend.Service;
using Backend.Service.ExaminationAndPatientCard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;
using PatientWebApp.Validators;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly SurveyValidator _surveyValidator;

        private readonly IExaminationService _examinationService;
        private readonly ExaminationValidator _examinationValidator;


        public SurveyController(ISurveyService surveyService, IExaminationService examinationService)
        {
            _surveyService = surveyService;
            _surveyValidator = new SurveyValidator(surveyService);
            _examinationService = examinationService;
            _examinationValidator = new ExaminationValidator(_examinationService);
        }


        [HttpPost]
        public ActionResult AddSurvey(SurveyDTO surveyDTO)
        {
            try
            {
                _surveyValidator.ValidateSurveyFields(surveyDTO);
                _examinationValidator.CheckIfExaminationIsFinished(surveyDTO.ExaminationId);
                _examinationValidator.CheckIfSurveyAboutExaminationIsCompleted(surveyDTO.ExaminationId);
                _surveyService.AddSurvey(SurveyMapper.SurveyDTOToSurvey(surveyDTO));
                _examinationService.CompleteSurveyAboutExamination(surveyDTO.ExaminationId);
                return Ok();
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


        [HttpGet("surveyResultAboutMedicalStaff")]
        public IActionResult GetSurveyResultAboutMedicalStaff()
        {
            List<SurveyResult> surveyResult = new List<SurveyResult>();
            try
            {
                surveyResult = _surveyService.GetSurveyResultsAboutMedicalStaff();
                return Ok(surveyResult);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }


        [HttpGet("surveyResultAboutDoctor/{jmbg}")]
        public IActionResult GetSurveyResultAboutDoctor(string jmbg)
        {
            List<SurveyResult> surveyResult = new List<SurveyResult>();
            try
            {
                surveyResult = _surveyService.GetSurveyResultsAboutDoctor(jmbg);
                return Ok(surveyResult);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }


        [HttpGet("surveyResultAboutHospital")]
        public IActionResult GetSurveyResultAboutHospital()
        {
            List<SurveyResult> surveyResult = new List<SurveyResult>();
            try
            {
                surveyResult = _surveyService.GetSurveyResultsAboutHospital();
                return Ok(surveyResult);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

    }
}
