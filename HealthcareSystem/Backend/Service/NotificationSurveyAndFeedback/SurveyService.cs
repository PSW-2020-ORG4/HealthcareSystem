/***********************************************************************
 * Module:  SurveyService.cs
 * Author:  Jelena Zeljko
 * Purpose: Definition of the Class Service.SurveyService
 ***********************************************************************/

using Backend.Model;
using Backend.Model.Exceptions;
using Backend.Repository;
using Backend.Service;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.NotificationSurveyAndFeedback
{
    public class SurveyService : ISurveyService
    {
        private ISurveyRepository _surveyRepository;

        public SurveyService(ISurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public void AddSurvey(Survey survey)
        {
            _surveyRepository.AddSurvey(survey);
        }

        public Survey GetSurveyById(int id)
        {
            Survey survey = _surveyRepository.GetSurveyById(id);
            if (survey == null)
                throw new NotFoundException("Survey with id=" + id + " doesn't exist in database.");
            return survey;
        }

        public List<Survey> GetSurveysByJmbg(string jmbg)
        {
            return _surveyRepository.GetSurveysByJmbg(jmbg);
        }

        public void UpdateSurvey(Survey survey)
        {
            _surveyRepository.UpdateSurvey(survey);
        }
    }
}
