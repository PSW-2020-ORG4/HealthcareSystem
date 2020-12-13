using Backend;
using Model.Enums;
using Model.Manager;
using Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditorServerTests.DataFactory
{
   public class CreateDoctor
    {
        private readonly CreateRoom _createRoom = new CreateRoom();
        public CreateDoctor(CreateRoom createRoom)
        {
            _createRoom = createRoom;
        }
        public Doctor CreateValidTestObject()
        {
            return new Doctor(jmbg: "0909965768767", name: "Ana", surname: "Markovic", dateOfBirth: DateTime.Now, gender: GenderType.F,
            city: new City(zipCode: 21000, name: "Novi Sad", country: new Country(id: 1, name: "Serbia")), homeAddress: "Zmaj Jovina 10", phone: "065452102", email: "pera@gmail.com", username: "pera",
            password: "12345678", numberOfLicence: "", doctorsOffice: _createRoom.CreateRooms()[0], dateOfEmployment: DateTime.Now);
        }
    }
}
