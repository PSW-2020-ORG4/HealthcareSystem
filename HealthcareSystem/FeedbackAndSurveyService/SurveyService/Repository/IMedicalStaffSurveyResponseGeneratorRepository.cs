using FeedbackAndSurveyService.SurveyService.Model;

namespace FeedbackAndSurveyService.SurveyService.Repository
{
    public interface IMedicalStaffSurveyResponseGeneratorRepository
    {
        SurveyReportGenerator Get();
    }
}
