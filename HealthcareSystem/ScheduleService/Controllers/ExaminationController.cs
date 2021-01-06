using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleService.DTO;

namespace ScheduleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {

        [HttpGet("finished/patient/{jmbg}")]
        public IActionResult GetFinishedExaminationByPatient(string jmbg)
        {
            throw new NotImplementedException();
        }

        [HttpGet("canceled/patient/{jmbg}")]
        public IActionResult GetCanceledExaminationByPatient(string jmbg)
        {
            throw new NotImplementedException();
        }

        [HttpGet("created/patient/{jmbg}")]
        public IActionResult GetCreatedExaminationByPatient(string jmbg)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult ScheduleExamination(ExaminationDTO examinationDTO)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{id}/cancel")]
        public IActionResult CancelExamination(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("search-free/basic")]
        public IActionResult BasicSearchExamination(BasicSearchDTO basicSearchDTO)
        {
            throw new NotImplementedException();
        }

        [HttpPost("search-free/advanced")]
        public IActionResult AdvancedSearchExamination(AdvancedSearchDTO advancedSearchDTO)
        {
            throw new NotImplementedException();
        }
    }
}
