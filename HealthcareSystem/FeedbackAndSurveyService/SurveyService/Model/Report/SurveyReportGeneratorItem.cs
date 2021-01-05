using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class SurveyReportGeneratorItem
    {
        private string Name { get; }
        private IEnumerable<Grade> Responses { get; }

        public double GetAverage()
        {
            throw new NotImplementedException();
        }

        public int GetGradeCount(Grade grade)
        {
            throw new NotImplementedException();
        }
    }
}
