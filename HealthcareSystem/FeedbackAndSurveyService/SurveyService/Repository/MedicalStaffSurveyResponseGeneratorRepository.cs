using Backend.Model;
using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.SurveyService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Repository
{
    public class MedicalStaffSurveyResponseGeneratorRepository : IMedicalStaffSurveyResponseGeneratorRepository
    {
        private readonly MyDbContext _context;

        public MedicalStaffSurveyResponseGeneratorRepository(MyDbContext context)
        {
            _context = context;
        }

        public SurveyReportGenerator Get()
        {
            try
            {
                List<SurveyReportGeneratorItem> items = new List<SurveyReportGeneratorItem>();
                if (_context.SurveysAboutMedicalStaff.Count() == 0)
                    throw new NotFoundException("No information available.");
                var behavoir = _context.SurveysAboutMedicalStaff.Select(s => new Grade(s.BehaviorOfMedicalStaff));
                items.Add(new SurveyReportGeneratorItem("Behavior", behavoir));
                var professionalism = _context.SurveysAboutMedicalStaff.Select(s => new Grade(s.MedicalStaffProfessionalism));
                items.Add(new SurveyReportGeneratorItem("Professionalism", professionalism));
                var advice = _context.SurveysAboutMedicalStaff.Select(s => new Grade(s.GettingAdviceByMedicalStaff));
                items.Add(new SurveyReportGeneratorItem("Getting advice or help when " +
                                                        "needed from medical staff", advice));
                var followup = _context.SurveysAboutMedicalStaff.Select(s => new Grade(s.EaseInObtainingFollowUpInformation));
                items.Add(new SurveyReportGeneratorItem("Ease in obtaining follow " +
                                                        "up information and care", followup));
                return new SurveyReportGenerator(items);
            }
            catch (FeedbackAndSurveyServiceException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new DataStorageException(e.Message);
            }
        }
    }
}
