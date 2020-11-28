using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public interface ISurveyRepository
    {
        void AddSurvey(Survey survey);

        List<SurveyAboutDoctor> GetSurveysByDoctor(string jmbg);

        List<SurveyResult> GetSurveyResultsAboutMedicalStaff();

        List<SurveyResult> GetSurveyResultsAboutHospital();

        List<SurveyResult> GetSurveyResultsAboutDoctor(string jmbg);
    }
}
