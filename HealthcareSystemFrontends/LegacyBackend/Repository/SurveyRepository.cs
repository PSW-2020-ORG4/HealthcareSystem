/***********************************************************************
 * Module:  SurveyRepository.cs
 * Author:  LukaRA252017
 * Purpose: Definition of the Class Repository.SurveyRepository
 ***********************************************************************/

using Model.Patient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repository
{
    public class SurveyRepository
    {
        private string path;

        public SurveyRepository()
        {
            string fileName = "survey.json";
            path = Path.GetFullPath(fileName);
        }

        public Survey NewSurvey(Survey survey)
        {
            List<Survey> surveys = ReadFromFile();
            Survey searchSurvey = GetSurveyByJmbg(survey.JmbgOfPatient);

            if(searchSurvey != null)
            {
                return null;
            }

            surveys.Add(survey);
            WriteInFile(surveys);
            return survey;
        }

        public Survey GetSurveyByJmbg(string jmbg)
        {
            List<Survey> surveys = ReadFromFile();

            Survey surveyForPatientJmbg = null; 
            
            foreach (Survey survey in surveys)
            {
                if (survey.JmbgOfPatient.Equals(jmbg))
                {
                    surveyForPatientJmbg = survey;
                    break;
                }
            }

            return surveyForPatientJmbg;
        }

        public Survey SetSurvey(Survey survey)
        {
            List<Survey> surveys = ReadFromFile();

            foreach (Survey s in surveys)
            {
                if (s.JmbgOfPatient.Equals(survey.JmbgOfPatient))
                {
                    s.DoctorGrade = survey.DoctorGrade;
                    s.PrivacyGrade = survey.PrivacyGrade;
                    s.StaffGrade = survey.StaffGrade;
                    s.Content = survey.Content;
                    break;
                }
            }

            WriteInFile(surveys);
            return survey;
        }

        public List<Survey> ReadFromFile()
        {
            List<Survey> surveys;

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                surveys = JsonConvert.DeserializeObject<List<Survey>>(json);
            }
            else
            {
                surveys = new List<Survey>();
                WriteInFile(surveys);
            }

            return surveys;
        }
       
        public void WriteInFile(List<Survey> surveys)
        {
            string json = JsonConvert.SerializeObject(surveys);
            File.WriteAllText(path, json);
        }
        
    }
}
