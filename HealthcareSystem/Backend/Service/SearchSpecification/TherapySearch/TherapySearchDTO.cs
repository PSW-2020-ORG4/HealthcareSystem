using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.SearchSpecification.TherapySearch
{
    public class TherapySearchDTO
    {
        public DateTime? StartDate { get; set; }
        public LogicalOperator EndDateOperator { get; set; }
        public DateTime? EndDate { get; set; }
        public LogicalOperator DoctorSurnameOperator { get; set; }
        public string DoctorSurname { get; set; }
        public LogicalOperator DrugNameOperator { get; set; }
        public string DrugName { get; set; }

        public TherapySearchDTO(){
        }

        public TherapySearchDTO(DateTime startDate, LogicalOperator endOperator, DateTime endDate, LogicalOperator doctorOperator, string doctorSurname, LogicalOperator drugOperator, string drugName) {

            StartDate = startDate;
            EndDateOperator = endOperator;
            EndDate = endDate;
            DoctorSurnameOperator = doctorOperator;
            DoctorSurname = doctorSurname;
            DrugNameOperator = drugOperator;
            DrugName = drugName;
        }
    }

}
