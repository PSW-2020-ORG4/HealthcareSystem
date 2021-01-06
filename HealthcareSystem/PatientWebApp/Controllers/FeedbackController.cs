using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Exceptions;
using Backend.Service.NotificationSurveyAndFeedback;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.NotificationSurveyAndFeedback;
using Model.Users;
using PatientWebApp.DTOs;
using PatientWebApp.Validators;
using PatientWebApp.Adapters;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
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

        /// <summary>
        /// /adding new feedback to database
        /// </summary>
        /// <param name="feedbackDTO">an object to be added to the database</param>
        /// <returns>if alright returns code 200(Ok), if not 400(bed request)</returns>
        /// 
        [Authorize(Roles = UserRoles.Patient)]
        [HttpPost]
        public ActionResult AddFeedback(AddFeedbackDTO feedbackDTO)
        {
            var jmbg = HttpContext.User.FindFirst("Jmbg").Value;
            feedbackDTO.CommentatorJmbg = jmbg;

            var client = new RestClient("http://localhost:" + 56701);
            var request = new RestRequest("/api/feedback", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(feedbackDTO);
            var response = client.Execute(request);

            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }
        /// <summary>
        /// / getting all published feedbacks
        /// </summary>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        /// 
        [AllowAnonymous]
        [HttpGet("published")]
        public ActionResult GetPublishedFeedbacks()
        {
            var client = new RestClient("http://localhost:" + 56701);
            var request = new RestRequest("/api/feedback/published");
            var response = client.Execute(request);
            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }
        /// <summary>
        /// /getting all unpublished feedbacks
        /// </summary>
        /// <returns>if alright returns code 200(Ok), if not 404(not found)</returns>
        /// 
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("unpublished")]
        public ActionResult GetUnpublishedFeedbacks()
        {
            var client = new RestClient("http://localhost:" + 56701);
            var request = new RestRequest("/api/feedback/unpublished");
            var response = client.Execute(request);
            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }
        /// <summary>
        /// / updating feedbacks status (property: IsPublished) to published
        /// </summary>
        /// <param name="id">id of the object to be changed</param>
        /// <returns>if alright returns code 200(Ok), if not 400(bed request)</returns>
        /// 
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("{id}")]
        public ActionResult PublishFeedback(int id)
        {
            var client = new RestClient("http://localhost:" + 56701);
            var request = new RestRequest("/api/feedback/" + id + "/publish", Method.POST);
            var response = client.Execute(request);
            var contentResult = new ContentResult();

            contentResult.Content = response.Content;
            contentResult.ContentType = "application/json";
            contentResult.StatusCode = (int)response.StatusCode;

            return contentResult;
        }
    }
}
