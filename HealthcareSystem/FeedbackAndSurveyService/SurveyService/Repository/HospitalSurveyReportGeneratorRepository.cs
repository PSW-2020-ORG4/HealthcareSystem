using Backend.Model;
using FeedbackAndSurveyService.CustomException;
using FeedbackAndSurveyService.SurveyService.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeedbackAndSurveyService.SurveyService.Repository
{
    public class HospitalSurveyReportGeneratorRepository : IHospitalSurveyReportGeneratorRepository
    {
        private readonly MyDbContext _context;

        public HospitalSurveyReportGeneratorRepository(MyDbContext context)
        {
            _context = context;
        }

        public SurveyReportGenerator Get()
        {
            try
            {
                List<SurveyReportGeneratorItem> items = new List<SurveyReportGeneratorItem>();
                if (_context.SurveysAboutHospital.Count() == 0)
                    throw new NotFoundException("No information available.");
                var nursing = _context.SurveysAboutHospital.Select(s => new Grade(s.Nursing));
                items.Add(new SurveyReportGeneratorItem("Nursing", nursing));
                var cleanliness = _context.SurveysAboutHospital.Select(s => new Grade(s.Cleanliness));
                items.Add(new SurveyReportGeneratorItem("Cleanliness", cleanliness));
                var general = _context.SurveysAboutHospital.Select(s => new Grade(s.OverallRating));
                items.Add(new SurveyReportGeneratorItem("General rating", general));
                var medicationAndInstruments = _context.SurveysAboutHospital.Select(s => new Grade(s.SatisfiedWithDrugAndInstrument));
                items.Add(new SurveyReportGeneratorItem("Medication and instruments", medicationAndInstruments));
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
