using Backend.Model;
using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.SurveyService.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeedbackAndSurveyService.SurveyService.Repository
{
    public class DoctorSurveyReportGeneratorRepository : IDoctorSurveyReportGeneratorRepository
    {
        private readonly MyDbContext _context;

        public DoctorSurveyReportGeneratorRepository(MyDbContext context)
        {
            _context = context;
        }

        public SurveyReportGenerator Get(string id)
        {
            try
            {
                var surveys = _context.SurveysAboutDoctor.Where(s => s.Examination.DoctorJmbg == id);
                List<SurveyReportGeneratorItem> items = new List<SurveyReportGeneratorItem>();
                if (surveys.Count() == 0)
                    throw new NotFoundException("No information available.");
                var behavoir = surveys.Select(s => new Grade(s.BehaviorOfDoctor));
                items.Add(new SurveyReportGeneratorItem("Behavior", behavoir));
                var professionalism = surveys.Select(s => new Grade(s.DoctorProfessionalism));
                items.Add(new SurveyReportGeneratorItem("Professionalism", professionalism));
                var advice = surveys.Select(s => new Grade(s.GettingAdviceByDoctor));
                items.Add(new SurveyReportGeneratorItem("Getting advice or help when " +
                                                        "needed from doctor", advice));
                var availability = surveys.Select(s => new Grade(s.AvailabilityOfDoctor));
                items.Add(new SurveyReportGeneratorItem("Availability", availability));
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
