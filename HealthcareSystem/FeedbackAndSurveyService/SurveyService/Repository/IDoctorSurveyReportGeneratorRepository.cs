﻿using FeedbackAndSurveyService.SurveyService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Repository
{
    public interface IDoctorSurveyReportGeneratorRepository
    {
        SurveyReportGenerator Get(string id);
    }
}