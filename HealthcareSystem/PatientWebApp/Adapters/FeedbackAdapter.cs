using Model.Users;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.Adapters
{
    public class FeedbackAdapter
    {
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

        public static FeedbackDTO FeedbackToFeedbackDTO(Feedback feedback)
        {
            FeedbackDTO dto = new FeedbackDTO();
            dto.Comment = feedback.Comment;
            dto.Id = feedback.Id;
            dto.CommentatorJmbg = feedback.CommentatorJmbg;
            dto.CommentatorName = feedback.Commentator.Name;
            dto.CommentatorSurname = feedback.Commentator.Surname;
            dto.IsAllowedToPublish = feedback.IsAllowedToPublish;

            return dto;
        }
    }
}

