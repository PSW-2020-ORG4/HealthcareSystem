using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service.NotificationSurveyAndFeedback;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Users;
using PatientWebApp.Adapters;
using PatientWebApp.DTOs;
using PatientWebApp.Validators;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly FeedbackValidator _feedbackValidator;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
            _feedbackValidator = new FeedbackValidator(_feedbackService);
        }

        [HttpGet("{id}")]
        public IActionResult GetFeedbackById(int id)
        {
            try
            {
                Feedback feedback = _feedbackService.GetFeedbackById(id);
                return Ok(FeedbackAdapter.FeedbackToFeedbackDTO(feedback));
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPost]
        public ActionResult AddFeedback(FeedbackDTO feedbackDTO)
        {
            try
            {
                _feedbackValidator.validateFeedbacksFields(feedbackDTO);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            Feedback feedback = FeedbackAdapter.FeedbackDTOToFeedback(feedbackDTO);
            _feedbackService.AddFeedback(feedback);
            return Ok();

        }

        [HttpGet("published-feedbacks")]
        public ActionResult GetPublishedFeedbacks()
        {
            List<FeedbackDTO> feedbackDTOs = new List<FeedbackDTO>();
            _feedbackService.GetPublishedFeedbacks().ForEach(feedback => feedbackDTOs.Add(FeedbackAdapter.FeedbackToFeedbackDTO(feedback)));
            return Ok(feedbackDTOs);
        }

        [HttpGet("unpublished-feedbacks")]
        public ActionResult GetUnpublishedFeedbacks()
        {
            List<FeedbackDTO> feedbackDTOs = new List<FeedbackDTO>();
            _feedbackService.GetUnpublishedFeedbacks().ForEach(feedback => feedbackDTOs.Add(FeedbackAdapter.FeedbackToFeedbackDTO(feedback)));
            return Ok(feedbackDTOs);
        }

        [HttpPut("{id}")]
        public ActionResult PublishFeedback(int id)
        {
            try
            {
                _feedbackValidator.checkIfFeedbacksIsAllowedToPublish(id);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception.Message);
            }
            _feedbackService.PublishFeedback(id);
            return Ok();
        }
    }
}
