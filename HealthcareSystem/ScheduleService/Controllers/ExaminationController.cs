using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleService.DTO;
using ScheduleService.Mapper;
using ScheduleService.Services;

namespace ScheduleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly IExaminationService _examinationService;
        private readonly IAvailableExaminationService _availableExaminationService;

        public ExaminationController(IExaminationService examinationService, IAvailableExaminationService availableExaminationService)
        {
            _examinationService = examinationService;
            _availableExaminationService = availableExaminationService;
        }

        [HttpGet("finished/patient/{jmbg}")]
        public IActionResult GetFinishedExaminationByPatient(string jmbg)
        {
            var examinations = _examinationService.GetFinishedByPatient(jmbg).Select(e => e.ToScheduledExaminationDTO());
            return Ok(examinations);
        }

        [HttpGet("canceled/patient/{jmbg}")]
        public IActionResult GetCanceledExaminationByPatient(string jmbg)
        {
            var examinations = _examinationService.GetCanceledByPatient(jmbg).Select(e => e.ToScheduledExaminationDTO());
            return Ok(examinations);
        }

        [HttpGet("created/patient/{jmbg}")]
        public IActionResult GetCreatedExaminationByPatient(string jmbg)
        {
            var examinations = _examinationService.GetCreatedByPatient(jmbg).Select(e => e.ToScheduledExaminationDTO());
            return Ok(examinations);
        }

        [HttpPost]
        public IActionResult ScheduleExamination(ScheduleExaminationDTO examinationDTO)
        {
            _examinationService.Schedule(examinationDTO);
            return NoContent();
        }

        [HttpPost("{id}/cancel")]
        public IActionResult CancelExamination(int id)
        {
            _examinationService.Cancel(id);
            return NoContent();
        }

        [HttpPost("search-free/basic")]
        public IActionResult BasicSearchExamination(BasicSearchDTO basicSearchDTO)
        {
            var available = _availableExaminationService.BasicSearch(basicSearchDTO).Select(
                e => e.ToUnscheduledExaminationDTO());
            return Ok(available);
        }

        [HttpPost("search-free/advanced")]
        public IActionResult AdvancedSearchExamination(AdvancedSearchDTO advancedSearchDTO)
        {
            var available = _availableExaminationService.AdvancedSearch(advancedSearchDTO).Select(
                e => e.ToUnscheduledExaminationDTO());
            return Ok(available);
        }
    }
}
