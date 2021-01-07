using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.SurveyService.DTO;
using FeedbackAndSurveyService.SurveyService.Model.Memento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class SurveyResponder : IOriginator<SurveyResponderMemento>
    {
        private Jmbg Jmbg { get; }
        private ICollection<SurveyPermission> Permissions { get; }
        private ICollection<SurveyResponse> Responses { get; }

        public SurveyResponder(string jmbg, ICollection<SurveyPermission> permissions)
        {
            Jmbg = new Jmbg(jmbg);
            if (permissions != null)
                Permissions = permissions;
            else
                Permissions = new List<SurveyPermission>();
            Responses = new List<SurveyResponse>();
        }

        public void RespondToSurvey(int permissionId, SurveyResponseDTO surveyResponse)
        {
            SurveyPermission surveyPermission = Permissions.FirstOrDefault(p => p.Id == permissionId);
            if (surveyPermission is null)
                throw new ActionNotPermittedException("Permission id is not valid.");
            Permissions.Remove(surveyPermission);
            Responses.Add(new SurveyResponse(surveyPermission, surveyResponse));
        }

        public SurveyResponderMemento GetMemento()
        {
            return new SurveyResponderMemento()
            {
                Jmbg = Jmbg.Value,
                Permissions = Permissions,
                Responses = Responses
            };
        }
    }
}
