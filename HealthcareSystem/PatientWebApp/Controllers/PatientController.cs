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
  
        }
    }
}
