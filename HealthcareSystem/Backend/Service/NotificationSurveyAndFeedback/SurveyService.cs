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
            try
            {
                _surveyRepository.AddSurvey(survey);
            }
            catch (Exception)
            {
                throw new DatabaseException("Error adding survey to database");
            }
        }

        public List<SurveyResult> GetSurveyResultsAboutDoctor(string jmbg)
        {
            List<SurveyResult> surveyResults = _surveyRepository.GetSurveyResultsAboutDoctor(jmbg);
            if (surveyResults == null)
                throw new NotFoundException("Survey results about doctor doesn't exist in database.");
            return surveyResults;
        }

        public List<SurveyResult> GetSurveyResultsAboutHospital()
        {
            List<SurveyResult> surveyResults = _surveyRepository.GetSurveyResultsAboutHospital();
            if (surveyResults == null)
                throw new NotFoundException("Survey results about hospital doesn't exist in database.");
            return surveyResults;
        }

        public List<SurveyResult> GetSurveyResultsAboutMedicalStaff()
        {
            List<SurveyResult> surveyResults = _surveyRepository.GetSurveyResultsAboutMedicalStaff();
            if (surveyResults == null)
                throw new NotFoundException("Survey results doesn't exist in database.");
            return surveyResults;
        }

    }
}
