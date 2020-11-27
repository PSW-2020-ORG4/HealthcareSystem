using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.DTO
{
    class TherapyDTO
    {
        public string Anamnesis { get; set; }

        public string DrugName { get; set; }
        
        public int DailyDose { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public TherapyDTO(string anamensis, string drug, int dailyDose, string startDate, string endDate)
        {
            this.Anamnesis = anamensis;
            this.DrugName = drug;
            this.DailyDose = dailyDose;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}
