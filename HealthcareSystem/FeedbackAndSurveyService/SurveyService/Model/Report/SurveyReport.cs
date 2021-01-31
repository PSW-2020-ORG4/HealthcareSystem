using System.Collections.Generic;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class SurveyReport
    {
        public ICollection<SurveyReportItem> Items { get; }
        public double TotalAverage { get; set; }

        public SurveyReport()
        {
            Items = new List<SurveyReportItem>();
        }
    }
}
