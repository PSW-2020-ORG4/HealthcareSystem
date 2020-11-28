/***********************************************************************
 * Module:  SurveyController.cs
 * Author:  Jelena Zeljko
 * Purpose: Definition of the Class Controller.SurveyController
 ***********************************************************************/


using Backend.Model;
using Model;
using Service.NotificationSurveyAndFeedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.NotificationSurveyAndFeedback
{
    public class SurveyController
    {
        private SurveyService _surveyService;

        public void AddSurvey(Survey survey)
        {
            _surveyService.AddSurvey(survey);
        }

        public List<Survey> ViewSurveyByJmbg(string jmbg)
        {
            return _surveyService.GetSurveysByJmbg(jmbg);
        }

        public void EditSurvey(Survey survey)
        {
            _surveyService.UpdateSurvey(survey);
        }

    }
}
