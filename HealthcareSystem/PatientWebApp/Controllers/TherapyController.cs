using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service.DrugAndTherapy;
using Backend.Service.ExaminationAndPatientCard;
using Backend.Service.SearchSpecification.TherapySearch;
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
        /// /getting searched therapies linked to a certain patient
        /// </summary>
        /// <param name="therapySearchDTO">an object need be find in the database</param>
        /// <returns>if alright returns code 200(Ok), if not 400(not found)</returns>
        [HttpPost("advance-search")]
        public ActionResult AdvanceSearchTherapies(TherapySearchDTO therapySearchDTO)
        {
            try
            {
                List<TherapyDTO> therapyDTOs = new List<TherapyDTO>();
                _therapyService.AdvancedSearch(therapySearchDTO).ForEach(therapy => therapyDTOs.Add(TherapyMapper.TherapyToTherapyDTO(therapy)));
                return Ok(therapyDTOs);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }

        }
    }
}
