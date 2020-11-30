/***********************************************************************
 * Module:  SurveyService.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Service.SurveyService
 ***********************************************************************/

using Model.Patient;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.NotificationSurveyAndFeedback
{
    public class SurveyService
    {
        private SurveyRepository surveyRepository = new SurveyRepository();

        public Survey AddSurvey(Survey survey)
        {
            return surveyRepository.NewSurvey(survey);
        }

        public Survey ViewSurveyByJmbg(string jmbg)
        {
            return surveyRepository.GetSurveyByJmbg(jmbg);
        }

        public Survey EditSurvey(Survey survey)
        {
            return surveyRepository.SetSurvey(survey);
        }
    }
}
