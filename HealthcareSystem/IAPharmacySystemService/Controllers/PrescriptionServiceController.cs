using Backend.Model.Users;
using IAPharmacySystemService.DTO;
using IntegrationAdaptersPharmacySystemService.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IntegrationAdaptersPharmacySystemService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrescriptionServiceController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PrescriptionServiceController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        [Route("get/patients")]
        public IActionResult GetPatients()
        {
            List<Patient> patients = _patientService.ViewPatients();
            List<PatientDto> patientsDto = new List<PatientDto>();
            foreach (var patient in patients)
            {
                patientsDto.Add(new PatientDto() { Name = patient.Name, Surname = patient.Surname, Jmbg = patient.Jmbg });
            }
            return Ok(patientsDto);
        }
    }
}
