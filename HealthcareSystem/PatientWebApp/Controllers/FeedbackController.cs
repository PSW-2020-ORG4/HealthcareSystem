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

        [HttpPost("addFeedback")]
        public void AddFeedback(FeedbackDTO feedbackDTO)
        {
            Feedback feedback = FeedbackAdapter.FeedbackDTOToFeedback(feedbackDTO);
            _feedbackService.AddFeedback(feedback);
        }
    }
}
