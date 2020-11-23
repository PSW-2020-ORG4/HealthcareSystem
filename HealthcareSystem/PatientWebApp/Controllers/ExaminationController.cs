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
        /*
        [HttpGet("{id}")]
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
        }*/
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
            catch (Exception)
            {
                return NotFound();
            }
        }
      
    }
}
