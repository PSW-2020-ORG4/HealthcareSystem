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



        [HttpPost("schedule/")]
        public ActionResult ScheduleAppointmentByDoctor([FromBody] ExaminationDTO scheduleExaminationDTO)
        {
            Examination scheduleExamination = new ExaminationMappers().ExmainationDTO_To_Examination(scheduleExaminationDTO);
            _scheduleAppintmentService.ScheduleAnAppointmentByDoctor(scheduleExamination);
            return Ok();
        }

        [HttpPost]
        public ActionResult GetFreeAppointments(AppointmentSearchWithPrioritiesDTO appointmentDTO)
        {
            Console.WriteLine("tu");
            List<Examination> examinations = (List<Examination>) _freeAppointmentSearchService.SearchWithPriorities(appointmentDTO);
            List<ExaminationDTO> allExaminations = new List<ExaminationDTO>();
            foreach(Examination e in examinations)
                allExaminations.Add(ExaminationMapper.Exmaination_To_ExaminationDTO(e));          
            
            return Ok(allExaminations);
        }
    }
}