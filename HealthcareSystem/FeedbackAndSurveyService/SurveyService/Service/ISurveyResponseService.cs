using FeedbackAndSurveyService.SurveyService.DTO;
using FeedbackAndSurveyService.SurveyService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Service
{
    public interface ISurveyResponseService
    {
        void RecordResponse(string jmbg, int permissionId, SurveyResponseDTO response);
        IEnumerable<SurveyPermission> GetPermissions(string jmbg);
    }
}
