using Backend;
using Model.Enums;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatientWebAppTests.CreateObjectsForTests
{
    public class CreateAdmin
    {
        public Admin CreateValidTestObject()
        {
            return new Admin(jmbg: "0811965521021", name: "Milan", surname: "Milic", dateOfBirth: DateTime.Now, gender: GenderType.M,
                            city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Srbija")),
                            homeAddress: "Zmaj Jovina 10", phone: "065444172", email: "milic_milan@gmail.com", 
                            username: "milic_milan@gmail.com", password: "milanmilic965");
        }
    }
}
