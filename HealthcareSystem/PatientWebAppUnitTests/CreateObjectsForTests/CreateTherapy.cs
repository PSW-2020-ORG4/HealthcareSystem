using System;
using System.Collections.Generic;
using System.Text;
using Model.Enums;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateTherapy : ICreateTestObject<Therapy>
    {
        public Therapy CreateValidTestObject()
        {
            return new Therapy(drug : new Drug(), examination : new Examination(id: 1, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: DateTime.Now, anamnesis: "Bol u grlu", doctor: new Doctor(), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: Model.Enums.BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "", medicalHistory: "",
                hasInsurance: true, lbo: "", patientJmbg: "1309998775018")), id: 1, diagnosis: "Angina", startDate: DateTime.Now, endDate: DateTime.Now, dailyDose:3);
        }

        public Therapy CreateInvalidTestObject()
        {
            return new Therapy(drug: new Drug(), examination: new Examination(id: 1, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: DateTime.Now, anamnesis: "Bol u grlu", doctor: new Doctor(), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: Model.Enums.BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "", medicalHistory: "",
                hasInsurance: true, lbo: "", patientJmbg: null)), id: 1, diagnosis: "Angina", startDate: DateTime.Now, endDate: DateTime.Now, dailyDose: 3);
        }
    }
}
