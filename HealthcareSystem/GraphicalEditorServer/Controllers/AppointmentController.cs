using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.DTO;
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

        public AppointmentController(IFreeAppointmentSearchService freeAppointmentSearchService, IScheduleAppointmenService scheduleAppintmentService)
        {
            _freeAppointmentSearchService = freeAppointmentSearchService;
            _scheduleAppintmentService = scheduleAppintmentService;
        }

        [HttpPost]
        public ActionResult GetFreeAppointments([FromBody] FrontAppointmentSearchDTO frontAppointmentDTO)
        {
            List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
            AppointmentSearchWithPrioritiesDTO appointmentDTO = new ExaminationMappers().FrontAppointmentSearchDTO_To_AppointmentSearchWithPrioritiesDTO(frontAppointmentDTO);
            foreach( Examination examination in (List<Examination>)_freeAppointmentSearchService.SearchWithPriorities(appointmentDTO))
            {
                examinationDTOs.Add(new ExaminationMappers().Exmaination_To_ExaminationDTO(examination));
            } 
            return Ok(examinationDTOs);
        }

        [HttpPut]
        public ActionResult GetFreeAppointmentsByPutMethod([FromBody] FrontAppointmentSearchDTO frontAppointmentDTO)
        {
            List<ExaminationDTO> examinationDTOs = new List<ExaminationDTO>();
            //method has not tested yet
           /* AppointmentSearchWithPrioritiesDTO appointmentDTO = new ExaminationMappers().FrontAppointmentSearchDTO_To_AppointmentSearchWithPrioritiesDTO(frontAppointmentDTO);
            foreach (Examination examination in (List<Examination>)_freeAppointmentSearchService.SearchWithPriorities(appointmentDTO))
            {
                examinationDTOs.Add(new ExaminationMappers().Exmaination_To_ExaminationDTO(examination));
            }*/
            //added to check if object will properly sent in front
            examinationDTOs.Add(new ExaminationDTO(DateTime.Now, "234134514", 2, 3));
            examinationDTOs.Add(new ExaminationDTO(DateTime.Now, "234134514", 2, 3));
            examinationDTOs.Add(new ExaminationDTO(DateTime.Now, "234134514", 2, 3));
            examinationDTOs.Add(new ExaminationDTO(DateTime.Now, "234134514", 2, 3));
            examinationDTOs.Add(new ExaminationDTO(DateTime.Now, "234134514", 2, 3));
            examinationDTOs.Add(new ExaminationDTO(DateTime.Now, "234134514", 2, 3));
            return Ok(examinationDTOs);
        }

        [HttpPost("schedule/")]
        public ActionResult ScheduleAppointmentByDoctor([FromBody] ExaminationDTO scheduleExaminationDTO)
        {
            Examination scheduleExamination = new ExaminationMappers().ExmainationDTO_To_Examination(scheduleExaminationDTO);
            _scheduleAppintmentService.ScheduleAnAppointmentByDoctor(scheduleExamination);
            return Ok();
        }

      /*  [HttpPost("proba/")]
        public ActionResult ScheduleAppointmentByDoctorProba()
        {
            Examination scheduleExamination = new Examination(DateTime.Now, "1309998775018", 1, 1);
            _scheduleAppintmentService.ScheduleAnAppointmentByDoctor(scheduleExamination);
            return Ok();
        }*/
    }
}