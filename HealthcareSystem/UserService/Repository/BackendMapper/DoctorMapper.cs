using Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;
using UserService.Model.Memento;

namespace UserService.Repository
{
    static class DoctorMapper
    {
        internal static DoctorAccount ToDoctorAccount(this Doctor d)
        {
            return new DoctorAccount(new DoctorAccountMemento()
            {
                Name = d.Name,
                Surname = d.Surname,
                UserType = UserType.Doctor,
                DateOfBirth = d.DateOfBirth.Value,
                Email = d.Email,
                HomeAddress = d.HomeAddress,
                Jmbg = d.Jmbg,
                Password = d.Password,
                Phone = d.Phone,
                Specialties = d.DoctorSpecialties.Select(s => new SpecialtyMemento()
                {
                    Id = s.SpecialtyId,
                    Name = s.Specialty.Name
                }),
                City = new CityMemento()
                {
                    Id = d.CityZipCode.Value,
                    Name = d.City.Name,
                    Country = new CountryMemento()
                    {
                        Id = d.City.CountryId,
                        Name = d.City.Country.Name
                    }
                },
                Gender = d.Gender.Value.ToGender()
            });
        }
    }
}
