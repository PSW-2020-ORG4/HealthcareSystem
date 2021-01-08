using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
