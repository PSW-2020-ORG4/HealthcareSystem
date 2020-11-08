using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Service.UsersAndWorkingTime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientWebApp.Adapters;
using PatientWebApp.DTOs;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public ActionResult GetAllPatients()
        {
            try
            {
                List<PatientDTO> patientDTOs = new List<PatientDTO>();
                _patientService.ViewPatients().ForEach(patient => patientDTOs.Add(PatientAdapter.PatientToPatientDTO(patient)));
                return Ok(patientDTOs);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
