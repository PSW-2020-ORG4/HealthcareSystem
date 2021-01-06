using System.Collections.Generic;
using System.Linq;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class SurveyReportGeneratorItem
    {
        public string Name { get; }
        private IEnumerable<Grade> Responses { get; }

        public SurveyReportGeneratorItem(string name, IEnumerable<Grade> responses)
        {
            Name = name;
            Responses = responses;
        }

        public double GetAverage()
        {
            return Responses.Select(r => r.Value).Average();
        }

        public int GetGradeCount(Grade grade)
        {
            return Responses.Where(r => r.Equals(grade)).Count();
        }
    }
}
