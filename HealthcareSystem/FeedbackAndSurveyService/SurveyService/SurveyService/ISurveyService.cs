using FeedbackAndSurveyService.SurveyService.DTO;
using FeedbackAndSurveyService.SurveyService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.SurveyService
{
    public interface ISurveyService
    {
        void RecordResponse(string jmbg, int permissionId, SurveyResponseDTO reponse);
        IEnumerable<SurveyPermission> GetPermissions(string jmbg);
        SurveyReportGenerator GetDoctorSurveyReport(string jmbg);
        SurveyReportGenerator GetHospitalSurveyReport();
        SurveyReportGenerator GetMedicalStaffSurveyReport();
    }
}
