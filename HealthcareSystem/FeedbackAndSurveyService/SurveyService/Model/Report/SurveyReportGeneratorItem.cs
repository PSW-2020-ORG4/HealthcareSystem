using System.Collections.Generic;
using System.Linq;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class SurveyReportGeneratorItem
    {
        public string Name { get; }
        private IEnumerable<Grade> Responses { get; }

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
