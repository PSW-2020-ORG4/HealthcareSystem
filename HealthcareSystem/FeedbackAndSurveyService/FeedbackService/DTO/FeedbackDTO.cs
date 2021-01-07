using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.FeedbackService.DTO
{
    public class FeedbackDTO
    {
        public int Id { get; set; }
        public DateTime SendingDate { get; set; }
        public string Comment { get; set; }
        public bool IsAnonymous { get; set; }
        public string CommentatorJmbg { get; set; }
        public string CommentatorName { get; set; }
        public string CommentatorSurname { get; set; }
        public bool IsAllowedToPublish { get; set; }
    }
}
