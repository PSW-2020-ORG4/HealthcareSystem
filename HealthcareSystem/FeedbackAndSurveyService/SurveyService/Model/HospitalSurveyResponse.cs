using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedbackAndSurveyService.SurveyService.Model
{
    public class HospitalSurveyResponse
    {
        public Grade Nursing { get; }
        public Grade Cleanliness { get; }
        public Grade General { get; }
        public Grade MedicationAndInstrumments { get; }

        public HospitalSurveyResponse(int nursing, int cleanliness, int general, int medicationAndInstrumments)
        {
            Nursing = new Grade(nursing);
            Cleanliness = new Grade(cleanliness);
            General = new Grade(general);
            MedicationAndInstrumments = new Grade(medicationAndInstrumments);
        }
    }
}
