﻿using Model.NotificationSurveyAndFeedback;
using Model.Users;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.Adapters
{
    public class FeedbackMapper
    {
        /// <summary>
        /// /addapting object type FeedbackDTO to type Feedback 
        /// </summary>
        /// <param name="dto">object type FeedbackDTO</param>
        /// <returns>object type Feedback</returns>
        public static Feedback FeedbackDTOToFeedback(FeedbackDTO dto)
        {
            Feedback feedback = new Feedback();
            feedback.Id = dto.Id;
            feedback.Comment = dto.Comment;
            feedback.CommentatorJmbg = dto.CommentatorJmbg;
            feedback.IsAllowedToPublish = dto.IsAllowedToPublish;
            feedback.IsPublished = false;

            return feedback;
        }
        /// <summary>
        /// /addapting object type Feedback to type FeedbackDTO
        /// </summary>
        /// <param name="feedback">object type Feedback</param>
        /// <returns>object type FeedbackDTO</returns>
        public static FeedbackDTO FeedbackToFeedbackDTO(Feedback feedback)
        {
            FeedbackDTO dto = new FeedbackDTO();
            dto.Comment = feedback.Comment;
            dto.Id = feedback.Id;
            dto.SendingDate = feedback.SendingDate.ToString("dd/MM/yyyy");

            dto.CommentatorJmbg = feedback.CommentatorJmbg;
            if (dto.CommentatorJmbg == null)
            {
                dto.CommentatorName = "";
                dto.CommentatorSurname = "";
            }
            else
            {
                dto.CommentatorName = feedback.Commentator.Name;
                dto.CommentatorSurname = feedback.Commentator.Surname;
            }
            dto.IsAllowedToPublish = feedback.IsAllowedToPublish;

            return dto;
        }
    }
}
