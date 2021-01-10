using System;

namespace PatientWebApp.DTOs
{
    public class FeedbackDTO
    {
        public int Id { get; set; }
        public string SendingDate { get; set; }
        public string Comment { get; set; }
        public bool IsAnonymous { get; set; }
        public string CommentatorJmbg { get; set; }
        public string CommentatorName { get; set; }
        public string CommentatorSurname { get; set; }
        public bool IsAllowedToPublish { get; set; }

    }
}
