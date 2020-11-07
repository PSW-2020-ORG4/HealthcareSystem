using Backend.Model.Exceptions;
using Backend.Service.NotificationSurveyAndFeedback;
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
                throw new BadRequestException("Feedback cannot be null.");
            if (feedbackDTO.Comment == null || feedbackDTO.Comment == "")
                throw new BadRequestException("Comment is required field.");
            if (feedbackDTO.CommentatorName == null)
                throw new BadRequestException("Commentator name cannot be null.");
            if (feedbackDTO.CommentatorSurname == null)
                throw new BadRequestException("Commentator surname cannot be null.");
        }

        public void checkIfFeedbacksIsAllowedToPublish(int id)
        {
            Feedback _feedback = _feedbackService.GetFeedbackById(id);
            if (!_feedback.IsAllowedToPublish)
                throw new BadRequestException("Feedback is not allowed to publish.");
        }
    }
}
