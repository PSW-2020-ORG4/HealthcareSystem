using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedbackAndSurveyService.FeedbackService.DTO;
using FeedbackAndSurveyService.FeedbackService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAndSurveyService.FeedbackService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private IFeedbackService _service;

        [HttpPost]
        public IActionResult Add(AddFeedbackDTO feedback)
        {
            _service.Add(feedback);
            return NoContent();
        }

        [HttpGet("published")]
        public IActionResult GetPublished()
        {
            throw new NotImplementedException();
        }

        [HttpGet("unpublished")]
        public IActionResult GetUnpublished()
        {
            throw new NotImplementedException();
        }

        [HttpPost("{id}/publish")]
        public IActionResult Publish(int id)
        {
            _service.Publish(id);
            return NoContent();
        }
    }
}