using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Model.Exceptions;
using Backend.Service;
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

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
            _surveyValidator = new SurveyValidator(surveyService);
        }

        [HttpPost]
        public ActionResult AddSurvey(SurveyDTO surveyDTO)
        {
            try
            {
                _surveyValidator.ValidateSurveyFields(surveyDTO);
                _surveyService.AddSurvey(SurveyMapper.SurveyDTOToSurvey(surveyDTO));
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

    }
}
