using FeedbackAndSurveyService.SurveyService.DTO;
using FeedbackAndSurveyService.SurveyService.Model;
using System.Collections.Generic;

namespace FeedbackAndSurveyService.SurveyService.Service
{
    public interface ISurveyResponseService
    {
        void RecordResponse(string jmbg, int permissionId, SurveyResponseDTO response);
        IEnumerable<SurveyPermission> GetPermissions(string jmbg);
    }
}
