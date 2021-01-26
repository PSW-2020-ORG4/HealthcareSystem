using FeedbackAndSurveyService.FeedbackService.DTO;
using FeedbackAndSurveyService.FeedbackService.Mapper;
using FeedbackAndSurveyService.FeedbackService.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FeedbackAndSurveyService.FeedbackService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private IFeedbackService _service;

        public FeedbackController(IFeedbackService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Add(AddFeedbackDTO feedback)
        {
            _service.Add(feedback);
            return NoContent();
        }

        [HttpGet("published")]
        public IActionResult GetPublished()
        {
            var published = _service.GetPublished().Select(f => f.ToFeedbackDTO());
            return Ok(published);
        }

        [HttpGet("unpublished")]
        public IActionResult GetUnpublished()
        {
            var unpublished = _service.GetUnpublished().Select(f => f.ToFeedbackDTO());
            return Ok(unpublished);
        }

        [HttpPost("{id}/publish")]
        public IActionResult Publish(int id)
        {
            _service.Publish(id);
            return NoContent();
        }
    }
}