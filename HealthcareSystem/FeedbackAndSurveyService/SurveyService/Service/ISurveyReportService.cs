using FeedbackAndSurveyService.SurveyService.DTO;
using FeedbackAndSurveyService.SurveyService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Service
{
    public interface ISurveyReportService
    {
        SurveyReport GetDoctorSurveyReport(string jmbg);
        SurveyReport GetHospitalSurveyReport();
        SurveyReport GetMedicalStaffSurveyReport();
    }
}
