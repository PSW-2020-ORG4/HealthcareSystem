using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.DTO;
using Backend.Service;
using Backend.Service.ExaminationAndPatientCard;
using GraphicalEditor.DTO;
using GraphicalEditorServer.DTO;
using GraphicalEditorServer.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.PerformingExamination;

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

       /* [HttpPost("emergency")]
        public ActionResult GetEmergencyAppointments(BasicAppointmentSearchDTO parameters)
        {
            List<Examination> unchangedExaminations = (List<Examination>)_freeAppointmentSearchService.GetUnchangedAppointmentsForEmergency(parameters);
            foreach(Examination e in unchangedExaminations)
            {
                if (e.ExaminationStatus == Backend.Model.Enums.ExaminationStatus.AVAILABLE)
                    return Ok(ExaminationMapper.Examination_To_ExaminationDTO(e));
            }

            List<Examination> shiftedExaminations = (List<Examination>)_freeAppointmentSearchService.GetShiftedAndSortedAppoinmentsForEmergency(parameters);

            return Ok(EmergencyExaminationMapper.Examinations_To_EmergencyExaminationDTO(unchangedExaminations, shiftedExaminations));
        }*/
    }
}