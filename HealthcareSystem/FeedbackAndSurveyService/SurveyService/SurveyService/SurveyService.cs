using FeedbackAndSurveyService.SurveyService.DTO;
using FeedbackAndSurveyService.SurveyService.Model;
using System;
using System.Collections.Generic;

namespace FeedbackAndSurveyService.SurveyService.SurveyService
{
    public class SurveyService : ISurveyService
    {
        public SurveyReportGenerator GetDoctorSurveyReport(string jmbg)
        {
            throw new NotImplementedException();
        }

        public SurveyReportGenerator GetHospitalSurveyReport()
        {
            throw new NotImplementedException();
        }

        public SurveyReportGenerator GetMedicalStaffSurveyReport()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SurveyPermission> GetPermissions(string jmbg)
        {
            throw new NotImplementedException();
        }

        public void RecordResponse(string jmbg, int permissionId, SurveyResponseDTO reponse)
        {
            throw new NotImplementedException();
        }
    }
}
