using Microsoft.AspNetCore.Mvc;
using PatientService.DTO;
using PatientService.Mapper;
using PatientService.Service;
using System.Linq;

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

        [HttpPost]
        public IActionResult Add(GuestPatientDTO guestPatient)
        {
            _service.Add(guestPatient);
            return NoContent();
        }

        [HttpGet("{jmbg}/medical-info")]
        public IActionResult Get(string jmbg)
        {
            var medicalInfo = _service.Get(jmbg).ToMedicalInfoDTO();
            return Ok(medicalInfo);
        }

        [HttpPut("{jmbg}/medical-info")]
        public IActionResult UpdateMedicalInfo(string jmbg, MedicalInfoUpdateDTO medicalInfoUpdate)
        {
            _service.UpdateMedicalInfo(jmbg, medicalInfoUpdate);
            return NoContent();
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

        [HttpGet("{jmbg}/examination/{id}/therapies")]
        public IActionResult GetTherapiesForExamination(string jmbg, int id)
        {
            var therapies = _service.GetTherapiesForExamination(jmbg, id).Select(t => t.ToTherapyDTO());
            return Ok(therapies);
        }

        [HttpGet("{jmbg}/examination/{id}")]
        public IActionResult GetExamination(string jmbg, int id)
        {
            var examination = _service.GetExamination(jmbg, id).ToExaminationDTO();
            return Ok(examination);
        }
    }
}