using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.DTO;
using UserService.Model;

namespace UserService.Mapper
{
    public static class PatientMapper
    {
        public static MaliciousPatientDTO ToMaliciousPatientDTO(this PatientAccount patient)
        {
            var memento = patient.GetPatientMemento();
            return new MaliciousPatientDTO()
            {
                Name = memento.Name,
                Surname = memento.Surname,
                Email = memento.Email,
                Jmbg = memento.Jmbg,
                HomeAddress = memento.HomeAddress,
                CityName = memento.City.Name,
                CountryName = memento.City.Country.Name,
                Phone = memento.Phone,
                IsBlocked = memento.IsBlocked,
                NumberOfMaliciousActions = memento.MaliciousActions.Count(),
                ImageName = memento.ImageName
            };
        }

        public static PatientDTO ToPatientDTO(this PatientAccount patient)
        {
            var memento = patient.GetPatientMemento();
            return new PatientDTO()
            {
                Name = memento.Name,
                Surname = memento.Surname,
                Email = memento.Email,
                Jmbg = memento.Jmbg,
                Phone = memento.Phone,
                DateOfBirth = memento.DateOfBirth,
                CityId = memento.City.Id,
                CityName = memento.City.Name,
                CountryId = memento.City.Country.Id,
                CountryName = memento.City.Country.Name,
                HomeAddress = memento.HomeAddress,
                ImageName = memento.ImageName,
                Gender = memento.Gender.ToString()
            };
        }
    }
}
