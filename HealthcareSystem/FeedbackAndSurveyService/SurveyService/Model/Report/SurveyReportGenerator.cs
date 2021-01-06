using System.Collections.Generic;
using System.Linq;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class SurveyReportGenerator
    {
        private IEnumerable<SurveyReportGeneratorItem> Items { get; }

        public SurveyReportGenerator(IEnumerable<SurveyReportGeneratorItem> items)
        {
            Items = items;
        }

        public SurveyReport GenerateReport()
        {
            SurveyReport surveyResult = new SurveyReport();

            foreach (SurveyReportGeneratorItem item in Items)
            {
                surveyResult.Items.Add(new SurveyReportItem()
                {
                    RatedItem = item.Name,
                    AverageRating = item.GetAverage(),
                    NumberOfGradesOne = item.GetGradeCount(new Grade(1)),
                    NumberOfGradesTwo = item.GetGradeCount(new Grade(2)),
                    NumberOfGradesThree = item.GetGradeCount(new Grade(3)),
                    NumberOfGradesFour = item.GetGradeCount(new Grade(4)),
                    NumberOfGradesFive = item.GetGradeCount(new Grade(5))
                });
            }
            surveyResult.TotalAverage = surveyResult.Items.Select(i => i.AverageRating).Average();

            return surveyResult;
        }
    }
}
