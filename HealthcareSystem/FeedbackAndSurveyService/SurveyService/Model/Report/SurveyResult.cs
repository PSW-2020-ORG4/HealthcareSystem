﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class SurveyResult
    {
        public ICollection<SurveyResultItem> Items { get; }
        public double TotalAverage { get; set; }

    }
}
