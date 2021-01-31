namespace PatientWebApp.DTOs
{
    public class AddFeedbackDTO
    {
        public string Comment { get; set; }
        public bool IsAnonymous { get; set; }
        public string CommentatorJmbg { get; set; }
        public bool IsAllowedToPublish { get; set; }
    }
}
