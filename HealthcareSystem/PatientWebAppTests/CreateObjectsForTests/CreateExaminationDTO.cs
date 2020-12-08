using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateExaminationDTO
    {
        public ExaminationDTO CreateValidTestObject()
        {
            return new ExaminationDTO(id: 8, type: "GENERAL", dateAndTime: DateTime.Now.AddDays(+2).ToString(), doctorJmbg: "0909965768767", doctorName: "Ana",
                                      doctorSurname: "Markovic", idRoom: 1, anamnesis: "glavobolja", patientJmbg: "1234567891234",
                                      patientCardId: 1, isSurveyCompleted: false);
        }

        public ExaminationDTO CreateInvalidTestObject()
        {
            return new ExaminationDTO(id: 1, type: "GENERAL", dateAndTime: "", doctorJmbg: "0909965768767", doctorName: "Ana",
                                      doctorSurname: "Markovic", idRoom: 1, anamnesis: "glavobolja", patientJmbg: "1234567891234",
                                      patientCardId: 1, isSurveyCompleted: false);
        }






    }
}
