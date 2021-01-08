using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PatientWebApp.Auth;
using PatientWebApp.DTOs;
using PatientWebApp.Settings;
using RestSharp;

namespace PatientWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly ServiceSettings _serviceSettings;

        public FeedbackController(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
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
            if (!feedbackDTO.IsAnonymous)
            {
                feedbackDTO.CommentatorJmbg = HttpContext.User.FindFirst("Jmbg").Value;
            }

            var client = new RestClient(_serviceSettings.FeedbackAndSurveyServiceUrl);
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
            var client = new RestClient(_serviceSettings.FeedbackAndSurveyServiceUrl);
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
            var client = new RestClient(_serviceSettings.FeedbackAndSurveyServiceUrl);
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
            var client = new RestClient(_serviceSettings.FeedbackAndSurveyServiceUrl);
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
