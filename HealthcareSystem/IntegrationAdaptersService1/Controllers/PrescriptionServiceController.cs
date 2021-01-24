using Backend.Model.Users;
using IntegrationAdaptersService1.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IntegrationAdaptersService1.Controllers
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
            foreach(var patient in patients)
            {
                patient.City.Users = null;
            }
            return Ok(patients);
        }
    }
}
