using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public ActionResult GetAllDoctors()
        {
            List<DoctorDTO> doctorDTOs = new List<DoctorDTO>();
            try
            {
                _doctorService.ViewDoctors().ForEach(doctor => doctorDTOs.Add(DoctorMapper.DoctorToDoctorDTO(doctor)));
                return Ok(doctorDTOs);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
