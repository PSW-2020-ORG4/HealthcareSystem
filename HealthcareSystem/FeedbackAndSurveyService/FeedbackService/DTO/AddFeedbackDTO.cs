namespace FeedbackAndSurveyService.FeedbackService.DTO
{
    public class AddFeedbackDTO
    {
        public string Comment { get; set; }
        public bool IsAnonymous { get; set; }
        public string CommentatorJmbg { get; set; }
        public bool IsAllowedToPublish { get; set; }
    }
}
