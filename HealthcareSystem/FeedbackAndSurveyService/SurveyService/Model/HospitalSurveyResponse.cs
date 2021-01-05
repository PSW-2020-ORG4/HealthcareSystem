using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class HospitalSurveyResponse
    {
        private Grade Nursing { get; }
        private Grade Cleanliness { get; }
        private Grade General { get; }
        private Grade MedicationAndInstrumments { get; }

        public HospitalSurveyResponse(int nursing, int cleanliness, int general, int medicationAndInstrumments)
        {
            Nursing = new Grade(nursing);
            Cleanliness = new Grade(cleanliness);
            General = new Grade(general);
            MedicationAndInstrumments = new Grade(medicationAndInstrumments);
        }
    }
}
