using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.DTOs
{
    public class FeedbackDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string CommentatorJmbg { get; set; }
        public string CommentatorName { get; set; }
        public string CommentatorSurname { get; set; }
        public bool IsAllowedToPublish { get; set; }

        public FeedbackDTO() { }
    }
}
