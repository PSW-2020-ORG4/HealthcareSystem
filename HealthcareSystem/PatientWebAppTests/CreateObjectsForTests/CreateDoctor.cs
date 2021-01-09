using Backend;
using Backend.Model.Enums;
using Model.Enums;
using Model.Manager;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateDoctor
    {
        public Doctor CreateValidTestObject()
        {
            return new Doctor(jmbg: "0909965768767", name: "Mira", surname: "Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F,
            city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Srbija")), homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
            password: "12345678", numberOfLicence: "", doctorsOffice: new Room(number: 1, typeOfUsage: TypeOfUsage.CONSULTING_ROOM, capacity: 1, occupation: 1, renovation: false), dateOfEmployment: DateTime.Now);
        }
    }
}
