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
        Survey GetSurveyById(int id);
        void AddSurvey(Survey survey);
        void UpdateSurvey(Survey survey);
        List<Survey> GetSurveysByJmbg(string jmbg);
    }
}
