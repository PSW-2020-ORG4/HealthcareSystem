using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Model.Users;
using Backend.Service;
using Backend.Service.UsersAndWorkingTime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Users;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IDoctorSpecialtyService _doctorSpecialtyService;

        public DoctorController(IDoctorService doctorService, ISpecialtyService specialtyService, IDoctorSpecialtyService doctorSpecialtyService)
        {
            _doctorService = doctorService;
            _specialtyService = specialtyService;
            _doctorSpecialtyService = doctorSpecialtyService;
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

        /// <summary>
        /// /getting doctor by jmbg
        /// </summary>
        /// <param name="jmbg">id of the wanted object</param>
        /// <returns>if alright returns code 200(Ok), if not 404(not found), if connection lost returns 500</returns>
        [HttpGet("{jmbg}")]
        public IActionResult GetDoctorByJmbg(string jmbg)
        {
            try
            {
                Doctor doctor = _doctorService.GetDoctorByJmbg(jmbg);
                return Ok(DoctorMapper.DoctorToDoctorDTO(doctor));
            }
            catch (DatabaseException e)
            {
                return StatusCode(500, e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// /getting specialtes
        /// </summary>
        /// <returns>if alright returns code 200(Ok), if connection lost returns 500</returns>
        [HttpGet("all-specialty")]
        public IActionResult GetAllSpecialtes()
        {
            List<SpecialtyDTO> specialtyDTOs = new List<SpecialtyDTO>();
            try
            {
                _specialtyService.GetSpecialties().ForEach(specialty => specialtyDTOs.Add(SpecialtyMapper.SpecialtyToSpecialtyDTO(specialty)));
                return Ok(specialtyDTOs);
            }
            catch (DatabaseException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// /getting doctorSpecialty by idSpecilaty
        /// </summary>
        /// <param name="id">id of the wanted object</param>
        /// <returns>if alright returns code 200(Ok), if connection lost returns 500</returns>
        [HttpGet("doctor-specialty/{id}")]
        public IActionResult GetDoctorSpecialtyBySpecialtyId(int id)
        {
            List<DoctorSpecialtyDTO> doctorSpecialtyDTOs = new List<DoctorSpecialtyDTO>();
            try
            {
                _doctorSpecialtyService.GetDoctorSpecialtyBySpecialtyId(id).ForEach(doctorSpecialty => doctorSpecialtyDTOs.Add(DoctorSpecialtyMapper.DoctorSpecialtyToDoctorSpecialtyDTO(doctorSpecialty)));
                return Ok(doctorSpecialtyDTOs);
            }
            catch (DatabaseException e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
