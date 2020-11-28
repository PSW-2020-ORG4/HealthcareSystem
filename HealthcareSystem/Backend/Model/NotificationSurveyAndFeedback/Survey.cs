using Backend.Model;
using Model.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class Survey
    {
        public SurveyAboutDoctor SurveyAboutDoctor { get; set; }
        public SurveyAboutMedicalStaff SurveyAboutMedicalStaff { get; set; }
        public SurveyAboutHospital SurveyAboutHospital { get; set; }

        public Survey() { }

        public Survey(SurveyAboutDoctor surveyAboutDoctor, 
                      SurveyAboutMedicalStaff surveyAboutMedicalStaff, 
                      SurveyAboutHospital surveyAboutHospital)
        {
            SurveyAboutDoctor = surveyAboutDoctor;
            SurveyAboutMedicalStaff = surveyAboutMedicalStaff;
            SurveyAboutHospital = surveyAboutHospital;
        }
    }
}
