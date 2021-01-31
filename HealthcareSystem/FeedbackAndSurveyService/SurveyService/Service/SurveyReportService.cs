using FeedbackAndSurveyService.SurveyService.Model;
using FeedbackAndSurveyService.SurveyService.Repository;

namespace FeedbackAndSurveyService.SurveyService.Service
{
    public class SurveyReportService : ISurveyReportService
    {
        private IDoctorSurveyReportGeneratorRepository _doctorRepository;
        private IMedicalStaffSurveyResponseGeneratorRepository _staffRepository;
        private IHospitalSurveyReportGeneratorRepository _hospitalRepository;

        public SurveyReportService(IDoctorSurveyReportGeneratorRepository doctorRepository,
                                    IMedicalStaffSurveyResponseGeneratorRepository staffRepository,
                                    IHospitalSurveyReportGeneratorRepository hospitalRepository)
        {
            _doctorRepository = doctorRepository;
            _staffRepository = staffRepository;
            _hospitalRepository = hospitalRepository;
        }

        public SurveyReport GetDoctorSurveyReport(string jmbg)
        {
            SurveyReportGenerator generator = _doctorRepository.Get(jmbg);
            return generator.GenerateReport();
        }

        public SurveyReport GetHospitalSurveyReport()
        {
            SurveyReportGenerator generator = _hospitalRepository.Get();
            return generator.GenerateReport();
        }

        public SurveyReport GetMedicalStaffSurveyReport()
        {
            SurveyReportGenerator generator = _staffRepository.Get();
            return generator.GenerateReport();
        }
    }
}
