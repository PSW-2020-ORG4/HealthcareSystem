using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Service.NotificationSurveyAndFeedback;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Users;
using PatientWebApp.Adapters;
using PatientWebApp.DTOs;

namespace PatientWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        /// <summary>
        /// /getting feedback by id
        /// </summary>
        /// <param name="id">id of the wanted object</param>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        [HttpGet("{id}")]
        public IActionResult GetFeedbackById(int id)
        {
            try
            {
                Feedback feedback = _feedbackService.GetFeedbackById(id);
                return Ok(FeedbackAdapter.FeedbackToFeedbackDTO(feedback));
            }
            catch (Exception) {
                return NotFound();
            }
        }
        /// <summary>
        /// /adding new feedback to database
        /// </summary>
        /// <param name="feedbackDTO">an object to be added to the database</param>
        /// <returns>if alright returns code 200(Ok), if not 400(bed request)</returns>
        [HttpPost]
        public ActionResult AddFeedback(FeedbackDTO feedbackDTO)
        {
            try
            {
                Feedback feedback = FeedbackAdapter.FeedbackDTOToFeedback(feedbackDTO);
                _feedbackService.AddFeedback(feedback);
                return Ok();
            } catch (Exception)
            {
                return BadRequest();
            }

        }
        /// <summary>
        /// / getting all published feedbacks
        /// </summary>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        [HttpGet("published-feedbacks")]
        public ActionResult getPublishedFeedbacks()
        {
            try
            {
                List<FeedbackDTO> feedbackDTOs = new List<FeedbackDTO>();
                _feedbackService.GetPublishedFeedbacks().ForEach(feedback => feedbackDTOs.Add(FeedbackAdapter.FeedbackToFeedbackDTO(feedback)));
                return Ok(feedbackDTOs);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        /// <summary>
        /// /getting all unpublished feedbacks
        /// </summary>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        [HttpGet("unpublished-feedbacks")]
        public ActionResult getUnpublishedFeedbacks()
        {
            try
            {
                List<FeedbackDTO> feedbackDTOs = new List<FeedbackDTO>();
                _feedbackService.GetUnpublishedFeedbacks().ForEach(feedback => feedbackDTOs.Add(FeedbackAdapter.FeedbackToFeedbackDTO(feedback)));
                return Ok(feedbackDTOs);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        /// <summary>
        /// / updating feedbacks status (property: IsPublished) to published
        /// </summary>
        /// <param name="id">id of the object to be changed</param>
        /// <returns>if alright returns code 200(Ok), if not 400(bed request)</returns>
        [HttpPut("{id}")]
        public ActionResult PublishFeedback(int id)
        {
            try
            {
                _feedbackService.PublishFeedback(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}
