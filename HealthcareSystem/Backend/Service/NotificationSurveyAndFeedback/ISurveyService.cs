using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public interface ISurveyService
    {
        void AddSurvey(Survey survey);

        public List<SurveyResult> GetSurveyResultsAboutMedicalStaff();

        public List<SurveyResult> GetSurveyResultsAboutDoctor(string jmbg);

        public List<SurveyResult> GetSurveyResultsAboutHospital();
    }
}
