using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;
using UserService.Model.Memento;

namespace UserService.Repository
{
    static class PatientMapper
    {
        internal static PatientAccountMemento ToPatientAccountMemento(this Patient patient)
        {
            return new PatientAccountMemento()
            {
                Jmbg = patient.Jmbg,
                Name = patient.Name,
                Surname = patient.Surname,
                DateOfBirth = patient.DateOfBirth.Value,
                ImageName = patient.ImageName,
                Email = patient.Email,
                Gender = patient.Gender.Value.ToGender(),
                HomeAddress = patient.HomeAddress,
                IsActivated = patient.IsActive,
                IsBlocked = patient.IsBlocked,
                Password = patient.Password,
                Phone = patient.Phone,
                UserType = UserType.Patient,
                City = new CityMemento()
                {
                    Id = patient.City.ZipCode,
                    Name = patient.City.Name,
                    Country = new CountryMemento()
                    {
                        Id = patient.City.CountryId,
                        Name = patient.City.Country.Name
                    }
                }
            };
        }

        internal static Patient ToBackendPatient(this PatientAccount patient)
        {
            var memento = patient.GetPatientMemento();
            return new Patient()
            {
                Jmbg = memento.Jmbg,
                DateOfBirth = memento.DateOfBirth,
                CityZipCode = memento.City.Id,
                DateOfRegistration = DateTime.Now,
                Email = memento.Email,
                Gender = memento.Gender.ToBackendGender(),
                HomeAddress = memento.HomeAddress,
                ImageName = memento.ImageName,
                IsActive = memento.IsActivated,
                IsBlocked = memento.IsBlocked,
                Name = memento.Name,
                IsGuest = false,
                Password = memento.Password,
                Phone = memento.Phone,
                Surname = memento.Surname,
                Username = memento.Email
            };
        }
    }
}
