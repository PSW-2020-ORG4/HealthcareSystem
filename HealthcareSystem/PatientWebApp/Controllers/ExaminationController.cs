using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service.ExaminationAndPatientCard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.PerformingExamination;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly IExaminationService _examinationService;
        public ExaminationController(IExaminationService examinationService)
        {
            _examinationService = examinationService;

        }
        /// <summary>
        /// /getting examinations linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        [HttpGet("{patientJmbg}")]
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
        /// /getting examinations linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        [HttpGet("{patientJmbg}/{startDate}/{endDate}/{doctorSurname}/{anamesis}/{firstOperator}/{secondOperator}/{thirdOperator}")]
        public ActionResult AdvancedSearchExaminations(string patientJmbg, string startDate, string endDate, string doctorSurname, string anamesis, string firstOperator, string secondOperator, string thirdOperator)
        {
            try
            {
                List<Examination> examinations = new List<Examination>();
                List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
                examinations = _examinationService.GetExaminationsByPatient(patientJmbg);
                _examinationService.AdvancedSearchExaminationReports(examinations, startDate, endDate, doctorSurname, anamesis, firstOperator, secondOperator, thirdOperator).ForEach(examination => examinationDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(examination)));
                return Ok(examinationDTOs);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

    }
}
