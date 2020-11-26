using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service.DrugAndTherapy;
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
    public class TherapyController : ControllerBase
    {
        private readonly ITherapyService _therapyService;
        public TherapyController(ITherapyService therapyService)
        {
            _therapyService = therapyService;
        }

        /// <summary>
        /// /getting therapies linked to a certain patient
        /// </summary>
        /// <param name="patientJmbg">patients jmbg</param>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        [HttpGet("{patientJmbg}")]
        public ActionResult GetTherapiesByPatient(string patientJmbg)
        {
            try
            {
                List<TherapyDTO> therapyDTOs = new List<TherapyDTO>();
                _therapyService.GetTherapyByPatient(patientJmbg).ForEach(therapy => therapyDTOs.Add(TherapyMapper.TherapyToTherapyDTO(therapy)));
                return Ok(therapyDTOs);
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
        [HttpGet("{patientJmbg}/{startDate}/{endDate}/{doctorSurname}/{drugName}/{firstOperator}/{secondOperator}/{thirdOperator}")]
        public ActionResult AdvancedSearchExaminations(string patientJmbg, string startDate, string endDate, string doctorSurname, string drugName, string firstOperator, string secondOperator, string thirdOperator)
        {
            try
            {
                List<Therapy> therapies = new List<Therapy>();
                List<TherapyDTO> therapyDTOs = new List<TherapyDTO>();
                therapies = _therapyService.GetTherapyByPatient(patientJmbg);    
                _therapyService.AdvancedSearchThearapiesReports(therapies, startDate, endDate, doctorSurname, drugName, firstOperator, secondOperator, thirdOperator).ForEach(therapy => therapyDTOs.Add(TherapyMapper.TherapyToTherapyDTO(therapy)));
                return Ok(therapyDTOs);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
