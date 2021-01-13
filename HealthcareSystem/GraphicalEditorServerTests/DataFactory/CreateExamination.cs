using Backend;
using Backend.Model.Enums;
using Backend.Model.PerformingExamination;
using Backend.Model.Users;
using Model.Enums;
using Model.Manager;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
    public class CreateExamination
    {
        public List<Examination> CreateInvalidTestObject1()
        {     
              return new List<Examination>() { new Examination(id: 1, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: new DateTime(2020, 12, 5, 7, 0, 0), anamnesis: "Bol u grlu", doctor: new Doctor(jmbg: "0909965768767", name: "Ana", surname: "Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F, city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                              homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                              password: "12345678", numberOfLicence: "", doctorsOffice: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                  capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                  capacity: 1, occupation: 1, renovation: false),
                  patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "",
                                   medicalHistory: "", hasInsurance: false, lbo: null, patientJmbg: "1234567891236"), false, ExaminationStatus.FINISHED) };
         }

        public Examination CreateValidTestObjectForMakingAnAppointemnt()
        {
            return new Examination(id: 2, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: new DateTime(2020, 12, 5, 7, 0, 0), anamnesis: "Bol u grlu", doctor: new Doctor(jmbg: "0909965768767", name: "Ana", surname: "Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F, city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                              homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                              password: "12345678", numberOfLicence: "", doctorsOffice: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                  capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                  capacity: 1, occupation: 1, renovation: false),
                  patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "",
                                   medicalHistory: "", hasInsurance: false, lbo: null, patientJmbg: "1234567891236"), false, ExaminationStatus.FINISHED) ;
        }

        public List<Examination> CreateInvalidTestObject2()
        {
            return new List<Examination>() { new Examination(id: 3, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: new DateTime(2020, 12, 5, 7, 30, 0), anamnesis: "Bol u grlu", doctor: new Doctor(jmbg: "0909965768767", name: "Ana", surname: "Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F, city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "12345678", numberOfLicence: "", doctorsOffice: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "",
                                 medicalHistory: "", hasInsurance: false, lbo: null, patientJmbg: "1234567891236"), false, ExaminationStatus.FINISHED) };
        }

        public List<Examination> CreateInvalidTestObject3()
        {
            return new List<Examination>() { new Examination(id: 4, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: new DateTime(2020, 12, 5, 8, 0, 0), anamnesis: "Bol u grlu", doctor: new Doctor(jmbg: "0909965768767", name: "Ana", surname: "Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F, city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "12345678", numberOfLicence: "", doctorsOffice: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "",
                                 medicalHistory: "", hasInsurance: false, lbo: null, patientJmbg: "1234567891236"), false, ExaminationStatus.FINISHED) };
        }

        public List<Examination> CreateTestObjectForEquipmentTransfer1()
        {
            return new List<Examination>() { new Examination(id: 4, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime:  new DateTime(2020, 12, 30, 8, 0, 0), anamnesis: "Bol u grlu", doctor: new Doctor(jmbg: "0909965768767", name: "Ana", surname: "Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F, city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "12345678", numberOfLicence: "", doctorsOffice: new Room(number: 9, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now), room: new Room(number: 9, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "",
                                 medicalHistory: "", hasInsurance: false, lbo: null, patientJmbg: "1234567891236"), false, ExaminationStatus.CREATED) };
        }

        public List<Examination> CreateTestObjectForEquipmentTransfer2()
        {
            return new List<Examination>() { new Examination(id: 4, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime:  new DateTime(2020, 12, 30, 8, 0, 0), anamnesis: "Bol u grlu", doctor: new Doctor(jmbg: "0909965768767", name: "Ana", surname: "Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F, city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "12345678", numberOfLicence: "", doctorsOffice: new Room(number: 15, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now), room: new Room(number: 15, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "",
                                 medicalHistory: "", hasInsurance: false, lbo: null, patientJmbg: "1234567891236"), false, ExaminationStatus.CREATED) };
        }

        public List<Examination> CreateTestObjectForEquipmentTransfer3()
        {
            return new List<Examination>() { new Examination(id: 5, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime:  new DateTime(2020, 12, 30, 8, 0, 0), anamnesis: "Bol u grlu", doctor: new Doctor(jmbg: "0909965768767", name: "Ana", surname: "Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F, city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "12345678", numberOfLicence: "", doctorsOffice: new Room(number: 20, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now), room: new Room(number: 20, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "",
                                 medicalHistory: "", hasInsurance: false, lbo: null, patientJmbg: "1234567891236"), false, ExaminationStatus.CREATED) };
        }

    }
}
