using System;
using System.Collections.Generic;
using System.Text;
using Backend.Service.SearchSpecification;
using Backend.Service.SearchSpecification.TherapySearch;
using Model.PerformingExamination;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateTherapySearchDTO: ICreateTestObject<TherapySearchDTO>
    {
        public TherapySearchDTO CreateValidTestObject()
        {
            return new TherapySearchDTO(jmbg: "1309998775018", startDate: DateTime.Now, endOperator: LogicalOperator.Or, endDate: DateTime.Now, doctorOperator: LogicalOperator.Or, doctorSurname: "Markovic", drugOperator: LogicalOperator.And, drugName: "Paracetamol");
        }

        public TherapySearchDTO CreateInvalidTestObject()
        {
            return new TherapySearchDTO(jmbg: "1309998775018", startDate: DateTime.Now, endOperator: LogicalOperator.And, endDate: DateTime.Now, doctorOperator: LogicalOperator.And, doctorSurname: null, drugOperator: LogicalOperator.And, drugName: null);
        }

    }
}
