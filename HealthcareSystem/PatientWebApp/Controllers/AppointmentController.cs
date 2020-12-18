using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.DTO;
using Backend.Model.Exceptions;
using Backend.Service;
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
        private readonly IDoctorService _doctorService;

        public AppointmentController(IFreeAppointmentSearchService freeAppointmentSearchService, IDoctorService doctorService)
        {
            _freeAppointmentSearchService = freeAppointmentSearchService;
            _doctorService = doctorService;
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
            parameters.EarliestDateTime = InitializeEarliestTime(parameters.EarliestDateTime);
            parameters.LatestDateTime = InitializeLatestTime(parameters.LatestDateTime);
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
        /// <summary>
        /// /getting free appointments
        /// </summary>
        /// <param name="parameters">parameters of priority search</param>
        /// <returns>if alright returns code 200(Ok), if object isn't valid returns 404, if connection lost returns 500</returns>
        [HttpPost("priority-search")]
        public IActionResult PrioritySearchAppointments(AppointmentSearchWithPrioritiesDTO parameters)
        {
            List<ExaminationDTO> freeAppointmentsDTOs = new List<ExaminationDTO>();
            List<Examination> freeAppointments;
            parameters.InitialParameters.EarliestDateTime = InitializeEarliestTime(parameters.InitialParameters.EarliestDateTime);
            parameters.InitialParameters.LatestDateTime = InitializeLatestTime(parameters.InitialParameters.LatestDateTime);
            try
            {
                parameters.InitialParameters.IsAppointmentValid();
                freeAppointments = _freeAppointmentSearchService.SearchWithPriorities(parameters).ToList();
                freeAppointments.ForEach(appointment => freeAppointmentsDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(appointment)));
                freeAppointmentsDTOs.ForEach(appointmentDTO => appointmentDTO.DoctorName = _doctorService.ViewProfile(appointmentDTO.DoctorJmbg).Name);
                freeAppointmentsDTOs.ForEach(appointmentDTO => appointmentDTO.DoctorSurname = _doctorService.ViewProfile(appointmentDTO.DoctorJmbg).Surname);
                return Ok(freeAppointmentsDTOs);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (BadRequestException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (DatabaseException exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
        private DateTime InitializeEarliestTime(DateTime earliest)
        {
            return new DateTime(earliest.Year, earliest.Month, earliest.Day, 7, 0, 0);
        }

        private DateTime InitializeLatestTime(DateTime latest)
        {
            return new DateTime(latest.Year, latest.Month, latest.Day, 17, 0, 0);
        }
    }
}
