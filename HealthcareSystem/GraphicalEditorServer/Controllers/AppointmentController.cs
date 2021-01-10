using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.DTO;
using Backend.Model.PerformingExamination;
using Backend.Model.Enums;
using Backend.Service;
using Backend.Service.ExaminationAndPatientCard;
using GraphicalEditor.DTO;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphicalEditorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IFreeAppointmentSearchService _freeAppointmentSearchService;
        private readonly IScheduleAppointmenService _scheduleAppintmentService;
        private readonly IDoctorService _doctorService;

        public AppointmentController(IFreeAppointmentSearchService freeAppointmentSearchService, IScheduleAppointmenService scheduleAppintmentService, IDoctorService doctorService)
        {
            _freeAppointmentSearchService = freeAppointmentSearchService;
            _scheduleAppintmentService = scheduleAppintmentService;
            _doctorService = doctorService;
        }



        [HttpPost("schedule/")]
        public ActionResult ScheduleAppointmentByDoctor([FromBody] ExaminationDTO scheduleExaminationDTO)
        {
            Examination scheduleExamination = ExaminationMapper.ExmainationDTO_To_Examination(scheduleExaminationDTO);
            int idExamination = _scheduleAppintmentService.ScheduleAnAppointmentByDoctor(scheduleExamination);
            return Ok(idExamination);
        }

        [HttpPost]
        public ActionResult GetFreeAppointments(AppointmentSearchWithPrioritiesDTO appointmentDTO)
        {
            List<Examination> examinations = (List<Examination>) _freeAppointmentSearchService.SearchWithPriorities(appointmentDTO);
            examinations.ForEach(e => e.Doctor = _doctorService.GetDoctorByJmbg(e.DoctorJmbg));
            List<ExaminationDTO> allExaminations = new List<ExaminationDTO>();
            examinations.ForEach(e => allExaminations.Add(ExaminationMapper.Examination_To_ExaminationDTO(e))); 
            
            return Ok(allExaminations);
        }

        [HttpPost("emergency")]
        public ActionResult GetEmergencyAppointments(AppointmentSearchWithPrioritiesDTO parameters)
        {
            List<Examination> unchangedExaminations = (List<Examination>)_freeAppointmentSearchService.GetUnchangedAppointmentsForEmergency(parameters);
            if(unchangedExaminations.Count != 0)
            {
                List<EmergencyExaminationDTO> sortedAndAlignedExamination = 
                    GetSortedAndAlignedExaminations(
                        new List<Examination>() { unchangedExaminations[0] }, 
                        new List<Examination>() { unchangedExaminations[0] }
                    );

                return Ok(new EmergencyExaminationsDTO(sortedAndAlignedExamination, false));
            }

            List<Examination> shiftedExaminations = (List<Examination>)_freeAppointmentSearchService.GetShiftedAndSortedAppoinmentsForEmergency(parameters);

            List<EmergencyExaminationDTO> sortedAndAlignedExaminations = GetSortedAndAlignedExaminations(unchangedExaminations, shiftedExaminations);
            return Ok(new EmergencyExaminationsDTO(sortedAndAlignedExaminations, true));
        }

        private List<EmergencyExaminationDTO> GetSortedAndAlignedExaminations(List<Examination> unchangedExaminations, List<Examination> shiftedExaminations)
        {
            List<EmergencyExaminationDTO> sortedExaminations = new List<EmergencyExaminationDTO>();
            for (int i = 0; i < unchangedExaminations.Count; i++)
            {
                sortedExaminations.Add(new EmergencyExaminationDTO(unchangedExaminations[i], shiftedExaminations[i]));
            }

            sortedExaminations.OrderBy(e => e.ShiftedExamination.DateTime).ToList();

            return sortedExaminations;
        }
    }
}