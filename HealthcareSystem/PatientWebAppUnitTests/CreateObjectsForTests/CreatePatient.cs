using Backend;
using Model.Enums;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreatePatient : ICreateObject
    {
        public object createValidObject()
        {
            return new Patient(jmbg: "1234567891234", name: "Pera", surname: "Peric", dateOfBirth: DateTime.Now, gender: GenderType.M,
                            city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
                            password: "pera", dateOfRegistration: DateTime.Now, isGuest: false);
        }
        public object createInvalidObject()
        {
            return new Patient(jmbg: null, name: "Mika", surname: "Mikic", dateOfBirth: DateTime.Now, gender: GenderType.M,
                            city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")),
                            homeAddress: null, phone: "065452102", email: null, username: "pera",
                            password: "pera", dateOfRegistration: DateTime.Now, isGuest: false);
        }   
    }
}
