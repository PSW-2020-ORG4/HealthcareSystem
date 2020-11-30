﻿using System;
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Nursing { get; set; }
        public int Cleanliness { get; set; }
        public int OverallRating { get; set; }
        public int SatisfiedWithDrugAndInstrument { get; set; }

        public SurveyAboutHospital() { }

        public SurveyAboutHospital(int nursing, int cleanliness, int overallRating, int satisfiedWithDrugAndInstrument)
        {
            Nursing = nursing;
            Cleanliness = cleanliness;
            OverallRating = overallRating;
            SatisfiedWithDrugAndInstrument = satisfiedWithDrugAndInstrument;
        }
    }
}