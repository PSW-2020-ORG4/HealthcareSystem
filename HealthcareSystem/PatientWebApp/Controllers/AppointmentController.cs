using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.DTO;
using Backend.Model.Exceptions;
using Backend.Service.ExaminationAndPatientCard;
using Microsoft.AspNetCore.Mvc;
using Model.PerformingExamination;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IFreeAppointmentSearchService _freeAppointmentSearchService;

        public AppointmentController(IFreeAppointmentSearchService freeAppointmentSearchService)
        {
            _freeAppointmentSearchService = freeAppointmentSearchService;
        }

        /// <summary>
        /// /getting free appointments
        /// </summary>
        /// <param name="parameters">parameters of basic search</param>
        /// <returns>if alright returns code 200(Ok), if object isn't valid returns 404, if connection lost returns 500</returns>
        [HttpPost("basic-search")]
        public IActionResult BasicSearchAppointments(BasicAppointmentSearchDTO parameters)
        {
            List<ExaminationDTO> freeAppointmentsDTOs = new List<ExaminationDTO>();
            List<Examination> freeAppointments;
            try
            {
                parameters.IsAppointmentValid();
                freeAppointments = _freeAppointmentSearchService.BasicSearch(parameters).ToList();
                freeAppointments.ForEach(appointment => freeAppointmentsDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(appointment)));
                return Ok(freeAppointmentsDTOs);
            }
            catch(ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch(BadRequestException exception)
            {
                return BadRequest(exception.Message);
            }
            catch(DatabaseException exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}
