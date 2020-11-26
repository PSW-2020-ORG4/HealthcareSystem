using Model.Enums;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateExaminationDTO : ICreateTestObject<ExaminationDTO>
    {
        public ExaminationDTO CreateInvalidTestObject()
        {
            
            return new ExaminationDTO(id: 1, type: "pregled", dateAndTime: "11.11.2020.", doctorJmbg: null, doctorName: "jelena", doctorSurname: "budisa",
               idRoom: 1,anamnesis: "", patientJmbg: null);
        }

        public ExaminationDTO CreateValidTestObject()
        {
            return new ExaminationDTO(id: 1, type: "pregled", dateAndTime: "11.11.2020.", doctorJmbg: null, doctorName: "jelena", doctorSurname: "budisa",
               idRoom: 1, anamnesis: "", patientJmbg: "22");
        }
    }
}
