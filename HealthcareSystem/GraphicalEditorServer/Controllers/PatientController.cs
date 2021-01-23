using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Backend.Model.Exceptions;
using Backend.Model.Users;
using Backend.Service;
using Backend.Service.Encryption;
using Backend.Service.SendingMail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Mappers;
using GraphicalEditorServer.Settings;
using Microsoft.Extensions.Options;
using GraphicalEditorServer.Controllers.Adapter;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IPatientCardService _patientCardService;

        private readonly ServiceSettings _serviceSettings;


        public PatientController(IPatientService patientService, IPatientCardService patientCardService, IOptions<ServiceSettings> serviceSettings)
        {
            _patientService = patientService;
            _patientCardService = patientCardService;
            _serviceSettings = serviceSettings.Value;
        }


        [HttpGet]
        public ActionResult GetAllPatients()
        {
            List<PatientBasicDTO> patientDTOs = new List<PatientBasicDTO>();
            try
            {
                foreach(Patient patient in _patientService.ViewPatients())
                {
                    PatientCard patientCard = _patientCardService.ViewPatientCard(patient.Jmbg);
                    PatientBasicDTO patientDTO = PatientMapper.PatientAndPatientCardToPatientBasicDTO(patient, patientCard);
                    patientDTOs.Add(patientDTO);
                }

                return Ok(patientDTOs);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }



        [HttpPost]
        public ActionResult AddPatientGuestAccount(GuestPatientDTO guestPatientDTO)
        {
            return RequestAdapter.SendRequestWithBody(_serviceSettings.PatientServiceUrl, "/api/patient", guestPatientDTO);
        }
    }
}
