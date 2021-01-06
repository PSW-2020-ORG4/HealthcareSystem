using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientService.DTO;
using PatientService.Mapper;
using PatientService.Service;

namespace PatientService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private IPatientService _service;

        public PatientController(IPatientService service)
        {
            _service = service;
        }

        [HttpGet("{jmbg}/medical-info")]
        public IActionResult Get(string jmbg)
        {
            var medicalInfo = _service.Get(jmbg).ToMedicalInfoDTO();
            return Ok(medicalInfo);
        }

        [HttpGet("{jmbg}/examination")]
        public IActionResult GetExaminations(string jmbg)
        {
            var examinations = _service.GetExaminations(jmbg).Select(e => e.ToExaminationDTO());
            return Ok(examinations);
        }

        [HttpPost("{jmbg}/examination/search")]
        public IActionResult GetExaminations(string jmbg, ExaminationSearchDTO examinationSearchDTO)
        {
            var examinations = _service.GetExaminations(jmbg, examinationSearchDTO).Select(e => e.ToExaminationDTO());
            return Ok(examinations);
        }

        [HttpGet("{jmbg}/therapy")]
        public IActionResult GetTherapies(string jmbg)
        {
            var therapies = _service.GetTherapies(jmbg).Select(t => t.ToTherapyDTO());
            return Ok(therapies);
        }

        [HttpPost("{jmbg}/therapy/search")]
        public IActionResult GetTherapies(string jmbg, TherapySearchDTO therapySearchDTO)
        {
            var therapies = _service.GetTherapies(jmbg, therapySearchDTO).Select(t => t.ToTherapyDTO());
            return Ok(therapies);
        }
    }
}