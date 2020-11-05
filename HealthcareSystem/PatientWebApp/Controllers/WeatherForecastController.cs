using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Service.NotificationSurveyAndFeedback;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Users;

namespace PatientWebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IFeedbackService _feedbackService;
        public WeatherForecastController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public string Get()
        {
            //GetFeedbackById(id) 
            Feedback feedback = _feedbackService.GetFeedbackById(1);
            return feedback.Comment; 
        }
    }
}
