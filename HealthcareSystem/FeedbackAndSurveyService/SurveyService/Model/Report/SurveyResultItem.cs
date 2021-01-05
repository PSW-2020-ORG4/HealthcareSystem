using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class SurveyResultItem
    {
        public string RatedItem { get; set; }
        public double AverageRating { get; set; }
        public int NumberOfGradesOne { get; set; }
        public int NumberOfGradesTwo { get; set; }
        public int NumberOfGradesThree { get; set; }
        public int NumberOfGradesFour { get; set; }
        public int NumberOfGradesFive { get; set; }
    }
}
