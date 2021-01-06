using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
