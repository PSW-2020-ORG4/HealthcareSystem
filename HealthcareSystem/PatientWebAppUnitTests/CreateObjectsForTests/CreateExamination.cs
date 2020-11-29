using Backend;
using Model.Enums;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateExamination : ICreateTestObject<Examination>
    {
       
        public Examination CreateValidTestObject()
        {
            return new Examination(id: 1, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: new DateTime(2020, 11, 11), anamnesis:"Bol u grlu", doctor: new Doctor(jmbg: "0909965768767",name:"Ana",surname:"Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F, city : new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "12345678", numberOfLicence: "", typeOfDoctor: TypeOfDoctor.Dermatolog, doctorsOffice: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now), room: new Room(number: 1,typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: Model.Enums.BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "",medicalHistory: "",
                hasInsurance: true, lbo: "",patientJmbg: "1309998775018"));
         }

        public Examination CreateSecondValidTestObject()
        {
            return new Examination(id: 1, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: DateTime.Now, anamnesis: "Bol u grlu", doctor: new Doctor(), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: Model.Enums.BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "", medicalHistory: "",
                hasInsurance: true, lbo: "", patientJmbg: "1309998775018"));
        }

      
              
        }
    }
}
