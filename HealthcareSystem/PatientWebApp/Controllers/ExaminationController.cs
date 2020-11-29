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
        
        [HttpGet]
        public IActionResult GetExaminationById(int id)
        {
            try
            {
                 Examination examination = _examinationService.GetExaminationById(id);
                 return Ok(ExaminationMapper.ExaminationToExaminationDTO(examination));
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
        /// /getting examinations linked to a certain patient for basic search
        /// </summary>
        /// <param name="patientJmbg"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="doctorSurname"></param>
        /// <param name="anamesis"></param>
        /// <returns> list od Examination DTOs</returns>
        [HttpGet("{patientJmbg}/{startDate}/{endDate}/{doctorSurname}/{anamesis}")]
        public ActionResult GetExaminationsByPatientSearch(string patientJmbg, string startDate, string endDate, string doctorSurname, string anamesis)
        {
            try
            {
                List<Examination> examinations = new List<Examination>();
                List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
                examinations = _examinationService.GetExaminationsByPatient(patientJmbg);
               _examinationService.GetExaminationsByPatientSearch(examinations, startDate, endDate, doctorSurname, anamesis).ForEach(examination => examinationDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(examination)));
                return Ok(examinationDTOs);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

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

    }
}
