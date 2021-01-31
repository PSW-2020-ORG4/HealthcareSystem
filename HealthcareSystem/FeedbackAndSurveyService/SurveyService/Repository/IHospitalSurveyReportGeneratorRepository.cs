using FeedbackAndSurveyService.SurveyService.Model;

namespace FeedbackAndSurveyService.SurveyService.Repository
{
    public interface IHospitalSurveyReportGeneratorRepository
    {
        SurveyReportGenerator Get();
    }
}
