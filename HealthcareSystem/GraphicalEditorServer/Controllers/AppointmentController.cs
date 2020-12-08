using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.DTO;
using Backend.Service.ExaminationAndPatientCard;
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

        public AppointmentController(IFreeAppointmentSearchService freeAppointmentSearchService)
        {
            _freeAppointmentSearchService = freeAppointmentSearchService;
        }

        [HttpPost]
        public ActionResult GetFreeAppointments([FromBody] AppointmentSearchWithPrioritiesDTO appointmentDTO)
        {
            List<Examination> examinations = (List<Examination>) _freeAppointmentSearchService.SearchWithPriorities(appointmentDTO);
            return Ok(examinations);
        }
    }
}