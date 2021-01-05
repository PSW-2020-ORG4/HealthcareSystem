using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Model.Users;
using Backend.Service;
using Backend.Service.UsersAndWorkingTime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Users;
using PatientWebApp.Constants;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
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

        [Authorize(Roles = UserRoles.Patient + "," + UserRoles.Admin)]
        [HttpGet]
        public ActionResult GetAllDoctors()
        {
            var client = new RestClient("http://localhost:" + ServerConstants.PORT);
            var request = new RestRequest("/api/doctor");
            var response = client.Execute(request);
            return StatusCode((int)response.StatusCode, response.Content);
        }

        /// <summary>
        /// /getting doctorSpecialty by idSpecilaty
        /// </summary>
        /// <param name="id">id of the wanted object</param>
        /// <returns>if alright returns code 200(Ok), if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpGet("doctor-specialty/{id}")]
        public IActionResult GetSpecialistDoctorsBySpecialtyId(int id)
        {          
            var client = new RestClient("http://localhost:" + ServerConstants.PORT);
            var request = new RestRequest("/api/doctor/specialty/" + id);
            var response = client.Execute(request);
            return StatusCode((int)response.StatusCode, response.Content);
        }
    }
}
