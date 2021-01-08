using Backend.Model.DTO;
using Backend.Model.Exceptions;
using Backend.Service;
using Backend.Service.ExaminationAndPatientCard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model.PerformingExamination;
using Model.Users;
using PatientWebApp.Auth;
using PatientWebApp.DTOs;
using PatientWebApp.Mappers;
using PatientWebApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IFreeAppointmentSearchService _freeAppointmentSearchService;
        private readonly IDoctorService _doctorService;
        private readonly ServiceSettings _serviceSettings;

        public AppointmentController(IFreeAppointmentSearchService freeAppointmentSearchService,
                                     IDoctorService doctorService,
                                     IOptions<ServiceSettings> serviceSettings)
        {
            _freeAppointmentSearchService = freeAppointmentSearchService;
            _doctorService = doctorService;
            _serviceSettings = serviceSettings.Value;
        }

        /// <summary>
        /// /getting free appointments
        /// </summary>
        /// <param name="parameters">parameters of basic search</param>
        /// <returns>if alright returns code 200(Ok), if object isn't valid returns 404, if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
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
                freeAppointments = ReduceFreeAppointments(freeAppointments);
                freeAppointments.ForEach(appointment => freeAppointmentsDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(appointment)));
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
        /// <summary>
        /// /getting free appointments
        /// </summary>
        /// <param name="parameters">parameters of priority search</param>
        /// <returns>if alright returns code 200(Ok), if object isn't valid returns 404, if connection lost returns 500</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
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
                freeAppointments = ReduceFreeAppointments(freeAppointments);
                freeAppointments.ForEach(appointment => freeAppointmentsDTOs.Add(ExaminationMapper.ExaminationToExaminationDTO(appointment)));
                SetDoctorNameAndSurname(freeAppointmentsDTOs);
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


        private void SetDoctorNameAndSurname(List<ExaminationDTO> freeAppointmentsDTOs)
        {
            foreach (ExaminationDTO dto in freeAppointmentsDTOs)
            {
                Doctor doctor = _doctorService.GetDoctorByJmbg(dto.DoctorJmbg);
                dto.DoctorSurname = doctor.Surname;
                dto.DoctorName = doctor.Name;
            }
        }

        private List<Examination> ReduceFreeAppointments(List<Examination> freeAppointments)
        {
            List<Examination> reduceFreeAppointments = new List<Examination>();
            foreach (Examination e in freeAppointments)
            {
                if (!reduceFreeAppointments.Where(r => DateTime.Compare(r.DateAndTime, e.DateAndTime) == 0).Any())
                {
                    reduceFreeAppointments.Add(e);
                }
            }
            return reduceFreeAppointments;

        }

    }
}
