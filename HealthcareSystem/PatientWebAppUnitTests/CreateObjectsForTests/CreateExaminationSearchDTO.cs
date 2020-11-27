using System;
using System.Collections.Generic;
using System.Text;
using Backend.Service.SearchSpecification;
using Backend.Service.SearchSpecification.ExaminationSearch;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateExaminationSearchDTO : ICreateTestObject<ExaminationSearchDTO>
    { 
        public ExaminationSearchDTO CreateValidTestObject()
        {
            return new ExaminationSearchDTO(jmbg: "1309998775018", startDate: DateTime.Now, endOperator: LogicalOperator.And, endDate: DateTime.Now, doctorOperator: LogicalOperator.And, doctorSurname: "Markovic", anamnesisOperator: LogicalOperator.And, anamnesis: "Bol u grlu");
        }

        public ExaminationSearchDTO CreateInvalidTestObject()
        {
            return new ExaminationSearchDTO(jmbg: null, startDate: DateTime.Now, endOperator: LogicalOperator.And, endDate: DateTime.Now, doctorOperator: LogicalOperator.And, doctorSurname: null, anamnesisOperator: LogicalOperator.And, anamnesis: null);
        }
    }
}
