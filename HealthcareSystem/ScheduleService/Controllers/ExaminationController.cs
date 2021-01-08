using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleService.DTO;
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
            return Ok(_examinationService.GetFinishedByPatient(jmbg));
        }

        [HttpGet("canceled/patient/{jmbg}")]
        public IActionResult GetCanceledExaminationByPatient(string jmbg)
        {
            return Ok(_examinationService.GetCanceledByPatient(jmbg));
        }

        [HttpGet("created/patient/{jmbg}")]
        public IActionResult GetCreatedExaminationByPatient(string jmbg)
        {
            return Ok(_examinationService.GetCreatedByPatient(jmbg));
        }

        [HttpPost]
        public IActionResult ScheduleExamination(ExaminationDTO examinationDTO)
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
            return Ok(_availableExaminationService.BasicSearch(basicSearchDTO));
        }

        [HttpPost("search-free/advanced")]
        public IActionResult AdvancedSearchExamination(AdvancedSearchDTO advancedSearchDTO)
        {
            return Ok(_availableExaminationService.AdvancedSearch(advancedSearchDTO));
        }
    }
}
