using Backend.Model.Exceptions;
using Backend.Service.NotificationSurveyAndFeedback;
using Model.NotificationSurveyAndFeedback;
using Model.Users;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.Validators
{
    public class FeedbackValidator
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbackValidator(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        public void validateFeedbacksFields(FeedbackDTO feedbackDTO)
        {
            if (feedbackDTO == null)
                throw new ValidationException("Feedback cannot be null.");
            if (feedbackDTO.Comment == null || feedbackDTO.Comment == "")
                throw new ValidationException("Comment is required field.");
            if (feedbackDTO.CommentatorName == null)
                throw new ValidationException("Commentator name cannot be null.");
            if (feedbackDTO.CommentatorSurname == null)
                throw new ValidationException("Commentator surname cannot be null.");
        }

        public void checkIfFeedbacksIsAllowedToPublish(int id)
        {
            Feedback feedback = _feedbackService.GetFeedbackById(id);
            if (!feedback.IsAllowedToPublish)
                throw new ValidationException("Feedback is not allowed to publish.");
        }
    }
}
