using FeedbackAndSurveyService.SurveyService.Model;

namespace FeedbackAndSurveyService.SurveyService.Repository
{
    public interface IDoctorSurveyReportGeneratorRepository
    {
        SurveyReportGenerator Get(string id);
    }
}
