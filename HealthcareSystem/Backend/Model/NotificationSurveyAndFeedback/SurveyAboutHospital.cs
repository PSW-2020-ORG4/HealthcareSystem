using Backend.Model.PerformingExamination;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class SurveyAboutHospital
    {
        private Examination examinationId;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Nursing { get; set; }
        public int Cleanliness { get; set; }
        public int OverallRating { get; set; }
        public int SatisfiedWithDrugAndInstrument { get; set; }

        [ForeignKey("Examination")]
        public int ExaminationId { get; set; }
        public virtual Examination Examination { get; set; }

        public SurveyAboutHospital() { }

        public SurveyAboutHospital(int nursing, int cleanliness, int overallRating, int satisfiedWithDrugAndInstrument, int examinationId)
        {
            Nursing = nursing;
            Cleanliness = cleanliness;
            OverallRating = overallRating;
            SatisfiedWithDrugAndInstrument = satisfiedWithDrugAndInstrument;
            ExaminationId = examinationId;
        }

        public SurveyAboutHospital(int nursing, int cleanliness, int overallRating, int satisfiedWithDrugAndInstrument, Examination examination)
        {
            Nursing = nursing;
            Cleanliness = cleanliness;
            OverallRating = overallRating;
            SatisfiedWithDrugAndInstrument = satisfiedWithDrugAndInstrument;
            Examination = examination;
        }
    }
}
