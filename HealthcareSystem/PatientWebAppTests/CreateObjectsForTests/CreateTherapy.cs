﻿using Backend;
using Backend.Model.Enums;
using Backend.Model.PerformingExamination;
using Backend.Model.Users;
using Model.Manager;
using Model.PerformingExamination;
using Model.Users;
using System;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateTherapy
    {
        public Therapy CreateValidTestObject()
        {
            return new Therapy(drug: new Drug(drugType: null, name: "Paracetamol", quantity: 20, expirationDate: new DateTime(2022, 11, 11), producer: "Hemofarm"), examination: new Examination(id: 1, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: new DateTime(2020, 11, 11), anamnesis: "Bol u grlu", doctor: new Doctor(jmbg: "0909965768767", name: "Ana", surname: "Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F, city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "12345678", numberOfLicence: "", doctorsOffice: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "", medicalHistory: "",
                hasInsurance: true, lbo: "", patientJmbg: "1309998775018")), id: 1, diagnosis: "Angina", startDate: new DateTime(2020, 11, 11), endDate: DateTime.Now, dailyDose: 3);
        }

        public Therapy CreateInvalidTestObject()
        {
            return new Therapy(drug: new Drug(), examination: new Examination(id: 1, typeOfExamination: TypeOfExamination.GENERAL, dateAndTime: DateTime.Now, anamnesis: "Bol u grlu", doctor: new Doctor(), room: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM,
                capacity: 1, occupation: 1, renovation: false),
                patientCard: new PatientCard(id: 1, bloodType: BloodType.A, rhFactor: RhFactorType.NEGATIVE, alergies: "", medicalHistory: "",
                hasInsurance: true, lbo: "", patientJmbg: null)), id: 1, diagnosis: "Angina", startDate: DateTime.Now, endDate: DateTime.Now, dailyDose: 3);
        }
    }
}
