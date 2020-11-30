/***********************************************************************
 * Module:  SurveyController.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Controller.SurveyController
 ***********************************************************************/


using Model.Patient;
using Service.NotificationSurveyAndFeedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.NotificationSurveyAndFeedback
{
    public class SurveyController
    {
        private SurveyService surveyService = new SurveyService();

        public Survey AddSurvey(Survey survey)
        {
            return surveyService.AddSurvey(survey);
        }

        public Survey ViewSurveyByJmbg(string jmbg)
        {
            return surveyService.ViewSurveyByJmbg(jmbg);
        }

        public Survey EditSurvey(Survey survey)
        {
            return surveyService.EditSurvey(survey);
        }

    }
}
