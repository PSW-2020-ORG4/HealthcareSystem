﻿using Backend;
using Backend.Model.Enums;
using Backend.Model.Users;
using Model.Users;
using System;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreatePatient
    {
        public Patient CreateValidTestObject()
        {
            return new Patient(jmbg: "1234567891234", name: "Pera", surname: "Peric", dateOfBirth: DateTime.Now, gender: GenderType.M,
                            city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "12345678", dateOfRegistration: DateTime.Now, isBlocked: false, null);
        }
        public Patient CreateInvalidTestObject()
        {
            return new Patient(jmbg: null, name: "Mika", surname: "Mikic", dateOfBirth: DateTime.Now, gender: GenderType.M,
                            city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: null, phone: "065452102", email: null, username: "pera",
                            password: "12345678", dateOfRegistration: DateTime.Now, isBlocked: false, null);
        }
    }
}
