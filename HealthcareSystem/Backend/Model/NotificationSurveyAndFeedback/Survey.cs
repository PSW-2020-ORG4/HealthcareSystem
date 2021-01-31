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
