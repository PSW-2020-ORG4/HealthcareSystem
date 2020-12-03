using Backend;
using Backend.Model.Users;
using Model.Enums;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;
using System;
using System.Collections.Generic;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateExamination
    {
        public Examination CreateValidTestObject()
        {
            return new Examination(id:1, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: new DateTime(2020, 11, 11), anamnesis:"Bol u grlu", doctor: new Doctor(jmbg: "0909965768767",name:"Ana",surname:"Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F, city : new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "12345678", numberOfLicence: "", doctorsOffice: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now), room: new Room(number: 1,typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "",medicalHistory: "",
                hasInsurance: true, lbo: "",patientJmbg: "1309998775018"), false);
         }
        public Examination CreateInvalidTestObject()
        {
            return new Examination(id: 2, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: DateTime.Now, anamnesis: null, doctor: new Doctor(), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
               capacity: 1, occupation: 1, renovation: false),
               patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "", medicalHistory: "",
               hasInsurance: true, lbo: "", patientJmbg: null));
        }

        public List<Examination> CreateValidTestObjects()
        {
            List<Examination> examinations = new List<Examination>();
            examinations.Add(new Examination(id: 1, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: new DateTime(2020, 11, 11), anamnesis: "Bol u grlu", doctor: new Doctor(jmbg: "0909965768767", name: "Ana", surname: "Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F, city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "12345678", numberOfLicence: "", doctorsOffice: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "", medicalHistory: "",
                hasInsurance: true, lbo: "", patientJmbg: "1309998775018"), false));

            examinations.Add(new Examination(id: 2, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: new DateTime(2020, 12, 01), anamnesis: "COVID-19", doctor: new Doctor(jmbg: "0909965768767", name: "Ana", surname: "Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F, city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "12345678", numberOfLicence: "", doctorsOffice: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "", medicalHistory: "",
                hasInsurance: true, lbo: "", patientJmbg: "1309998775018"), true));

            examinations.Add(new Examination(id: 3, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: new DateTime(2020, 12, 01), anamnesis: "Glavobolja", doctor: new Doctor(jmbg: "1254789652123", name: "Dara", surname: "Darić", dateOfBirth: DateTime.Now, gender: GenderType.F, city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "dara@gmail.com", username: "dara",
                            password: "12345678", numberOfLicence: "", doctorsOffice: new Room(number: 2, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "", medicalHistory: "",
                hasInsurance: true, lbo: "", patientJmbg: "1309998775018"), false));

            return examinations;
        }

    }
}
