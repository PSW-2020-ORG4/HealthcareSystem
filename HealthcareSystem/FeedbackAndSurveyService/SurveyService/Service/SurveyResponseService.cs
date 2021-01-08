using FeedbackAndSurveyService.SurveyService.DTO;
using FeedbackAndSurveyService.SurveyService.Model;
using FeedbackAndSurveyService.SurveyService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Service
{
    public class SurveyResponseService : ISurveyResponseService
    {
        private readonly ISurveyResponderRepository _repository;

        public SurveyResponseService(ISurveyResponderRepository repository)
        {
            _repository = repository;
        }

        public void RecordResponse(string jmbg, int permissionId, SurveyResponseDTO response)
        {
            SurveyResponder responder = _repository.Get(jmbg);
            responder.RespondToSurvey(permissionId, response);
            _repository.Update(responder);
        }

        public IEnumerable<SurveyPermission> GetPermissions(string jmbg)
        {
            SurveyResponder responder = _repository.Get(jmbg);
            return responder.GetMemento().Permissions;
        }
    }
}
