using System.Collections.Generic;

namespace FeedbackAndSurveyService.SurveyService.Model.Memento
{
    public class SurveyResponderMemento : IMemento
    {
        public string Jmbg { get; set; }
        public IEnumerable<SurveyPermission> Permissions { get; set; }
        public IEnumerable<SurveyResponse> Responses { get; set; }

        public SurveyResponderMemento()
        {
            Permissions = new List<SurveyPermission>();
            Responses = new List<SurveyResponse>();
        }
    }
}
