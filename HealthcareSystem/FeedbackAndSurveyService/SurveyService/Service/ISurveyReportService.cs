using FeedbackAndSurveyService.SurveyService.Model;

namespace FeedbackAndSurveyService.SurveyService.Service
{
    public interface ISurveyReportService
    {
        SurveyReport GetDoctorSurveyReport(string jmbg);
        SurveyReport GetHospitalSurveyReport();
        SurveyReport GetMedicalStaffSurveyReport();
    }
}
