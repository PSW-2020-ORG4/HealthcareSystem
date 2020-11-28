using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.SearchSpecification.ExaminationSearch
{
    public class ExaminationSearchDTO
    {
        public string Jmbg { get; set; }
        public DateTime? StartDate { get; set; }
        public LogicalOperator EndDateOperator { get; set; }
        public DateTime? EndDate { get; set; }
        public LogicalOperator DoctorSurnameOperator { get; set; }
        public string DoctorSurname { get; set; }
        public LogicalOperator AnamnesisOperator { get; set; }
        public string Anamnesis { get; set; }

        public ExaminationSearchDTO() { 
        }
        public ExaminationSearchDTO(string jmbg, DateTime startDate, LogicalOperator endOperator, DateTime endDate, LogicalOperator doctorOperator, string doctorSurname, LogicalOperator anamnesisOperator, string anamnesis) {

            Jmbg = jmbg;
            StartDate = startDate;
            EndDateOperator = endOperator;
            EndDate = endDate;
            DoctorSurnameOperator = doctorOperator;
            DoctorSurname = doctorSurname;
            AnamnesisOperator = anamnesisOperator;
            Anamnesis = anamnesis;
        }
    }
}
