/***********************************************************************
 * Module:  FeedbackService.cs
 * Author:  Sladjana Savkovic
 * Purpose: Definition of the Class Service.Notification&Survey&dFeedbackService.FeedbackService
 ***********************************************************************/

using Backend.Model.Exceptions;
using Backend.Repository;
using Backend.Service.NotificationSurveyAndFeedback;
using Model.Users;
using Repository;
using System;
using System.Collections.Generic;

namespace Service.NotificationSurveyAndFeedback
{
   public class FeedbackService : IFeedbackService
   {
        private IFeedbackRepository _feedbackRepository;
        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }
        /// <summary>
        /// /adding new feedback to database
        /// </summary>
        /// <param name="feedback">an object to be added to the database</param>
        public void AddFeedback(Feedback feedback)
        {
            _feedbackRepository.AddFeedback(feedback);
        }
        /// <summary>
        /// /updating feedbacks status (property: IsPublished) to published
        /// </summary>
        /// <param name="id">id of the object to be changed</param>
        public void PublishFeedback(int id)
        {
            Feedback feedback = GetFeedbackById(id);
            feedback.IsPublished = true;
            _feedbackRepository.UpdateFeedback(feedback);
        }
        /// <summary>
        /// /getting all published feedbacks
        /// </summary>
        /// <returns>list of published feedbacks</returns>
        public List<Feedback> GetPublishedFeedbacks()
        {
            return _feedbackRepository.GetPublishedFeedbacks();
        }
        /// <summary>
        /// /getting all unpublished feedbacks
        /// </summary>
        /// <returns>list of unpublished feedbacks</returns>
        public List<Feedback> GetUnpublishedFeedbacks()
        {
            return _feedbackRepository.GetUnpublishedFeedbacks();
        }
        /// <summary>
        /// /getting feedback by id
        /// </summary>
        /// <param name="id">id of the wanted object</param>
        /// <returns>object type Feedback</returns>
        public Feedback GetFeedbackById(int id)
        {
            Feedback feedback = _feedbackRepository.GetFeedbackById(id);
            if (feedback == null)
                throw new NotFoundException("Feeback with id=" + id + " doesn't exist in database.");
            return feedback;
        }
    }
}