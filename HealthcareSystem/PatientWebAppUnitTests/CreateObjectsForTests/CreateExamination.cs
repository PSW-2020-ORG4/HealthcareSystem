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
       
        Examination ICreateTestObject<Examination>.CreateValidTestObject()
        {
            return new Examination(id: 1, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: DateTime.Now, anamnesis: "", 
                doctor: new Doctor( jmbg: "2", name: "", surname: "budisa", dateOfBirth: DateTime.Now, gender: GenderType.F, city: null,homeAddress:"",phone:"",email:"",username:"",password:"",numberOfLicence:"",
                typeOfDoctor: TypeOfDoctor.Dermatolog,doctorsOffice:new Room(),dateOfEmployment: DateTime.Now),
                room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: Model.Enums.BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "", medicalHistory: "",
                hasInsurance: true, lbo: "", patientJmbg: "1"));
         }

        Examination ICreateTestObject<Examination>.CreateInvalidTestObject()
        {
            return new Examination(id: 2, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: DateTime.Now, anamnesis: "", doctor: new Doctor(jmbg: "2", name: "", surname: "", dateOfBirth: DateTime.Now, gender: GenderType.F, city: null, homeAddress: "", phone: "", email: "", username: "", password: "", numberOfLicence: "",
                typeOfDoctor: TypeOfDoctor.Dermatolog, doctorsOffice: new Room(), dateOfEmployment: DateTime.Now), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
               capacity: 1, occupation: 1, renovation: false),
               patientCard: new PatientCard(id: 1, bloodType: Model.Enums.BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "", medicalHistory: "",
               hasInsurance: true, lbo: "", patientJmbg: null));
        }
    }
}
