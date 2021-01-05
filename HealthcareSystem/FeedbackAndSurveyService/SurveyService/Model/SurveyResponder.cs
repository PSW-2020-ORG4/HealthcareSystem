using FeedbackAndSurveyService.SurveyService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class SurveyResponder
    {
        private Jmbg Jmbg { get; }
        private ICollection<SurveyPermission> Permissions { get; }
        private ICollection<SurveyResponse> Responses { get; }

        public void RespondToSurvey(int permissionId, SurveyResponseDTO surveyResponse)
        {
            SurveyPermission surveyPermission = (SurveyPermission)Permissions.Where(p => p.Id == permissionId);
            Permissions.Remove(surveyPermission);
            Responses.Add(new SurveyResponse(surveyPermission, surveyResponse));
        }
    }
}
