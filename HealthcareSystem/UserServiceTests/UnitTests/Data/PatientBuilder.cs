using System;
using System.Collections.Generic;
using UserService.Model;
using UserService.Model.Memento;

namespace UserServiceTests.UnitTests.Data
{
    public class PatientBuilder
    {
        private PatientAccountMemento _patientAccountMemento;
        public PatientBuilder()
        {
            _patientAccountMemento = new PatientAccountMemento()
            {
                Jmbg = "2506998524102",
                Name = "Pera",
                Surname = "Peric",
                Gender = Gender.Male,
                City = new CityMemento() { Id = 1, Name = "Novi Sad", Country = new CountryMemento() { Id = 1, Name = "Srbija" } },
                DateOfBirth = new DateTime(1998, 6, 25),
                Email = "pera.peric@gmail.com",
                HomeAddress = "Tolstojeva 12",
                ImageName = "1.jpg",
                IsActivated = false,
                IsBlocked = false,
                MaliciousActions = new List<MaliciousActionMemento>(),
                Password = "perapera123",
                Phone = "065895452",
                UserType = UserType.Patient
            };
        }

        public PatientAccount GetUnactivated()
        {
            _patientAccountMemento.IsActivated = false;
            _patientAccountMemento.IsBlocked = false;
            _patientAccountMemento.MaliciousActions = new List<MaliciousActionMemento>();

            return new PatientAccount(_patientAccountMemento);
        }

        public PatientAccount GetActivated(int numberOfMaliciousActions)
        {
            List<MaliciousActionMemento> maliciousActions = new List<MaliciousActionMemento>();
            _patientAccountMemento.IsActivated = true;
            _patientAccountMemento.IsBlocked = false;

            for (int i = 1; i <= numberOfMaliciousActions; i++)
            {
                maliciousActions.Add(new MaliciousActionMemento() { Id = i, TimeStamp = DateTime.Now, Type = MaliciousActionType.AppointmentCancellation });
            }

            _patientAccountMemento.MaliciousActions = maliciousActions;
            return new PatientAccount(_patientAccountMemento);
        }

        public PatientAccount GetBlocked()
        {
            _patientAccountMemento.IsActivated = true;
            _patientAccountMemento.IsBlocked = true;
            _patientAccountMemento.MaliciousActions = new List<MaliciousActionMemento>()
            {
                new MaliciousActionMemento() { Id = 1, TimeStamp = DateTime.Now, Type = MaliciousActionType.AppointmentCancellation },
                new MaliciousActionMemento() { Id = 2, TimeStamp = DateTime.Now, Type = MaliciousActionType.AppointmentCancellation },
                new MaliciousActionMemento() { Id = 3, TimeStamp = DateTime.Now, Type = MaliciousActionType.AppointmentCancellation },
                new MaliciousActionMemento() { Id = 4, TimeStamp = DateTime.Now, Type = MaliciousActionType.AppointmentCancellation }
            };

            return new PatientAccount(_patientAccountMemento);
        }
    }
}
