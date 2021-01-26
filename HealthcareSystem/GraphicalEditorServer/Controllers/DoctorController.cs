using Backend.Model.Exceptions;
using Backend.Service;
using Backend.Service.UsersAndWorkingTime;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Mappers;
using Microsoft.AspNetCore.Mvc;
using Model.Users;
using System.Collections.Generic;

namespace GraphicalEditorServer.Controllers
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


        [HttpGet("specialties")]
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


        [HttpGet("doctors-by-specialty/{id}")]
        public IActionResult GetDoctorsBySpecialtyId(int id)
        {
            List<DoctorDTO> doctorDTOs = new List<DoctorDTO>();
            try
            {
                foreach (Doctor doctor in _doctorService.ViewDoctorsBySpecialty(id))
                {
                    doctorDTOs.Add(DoctorMapper.DoctorToDoctorDTO(doctor));
                }

                return Ok(doctorDTOs);
            }
            catch (DatabaseException e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}

