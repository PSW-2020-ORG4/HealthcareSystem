using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Model;
using UserService.Model.Memento;

namespace UserService.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly Backend.Repository.IDoctorRepository _repository;

        public DoctorRepository(Backend.Repository.IDoctorRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DoctorAccount> GetBySpecialty(int specialtyId)
        {
            return _repository.GetDoctorsBySpecialty(specialtyId).Select(d => new DoctorAccount(new DoctorAccountMemento()
            {
                Name = d.Name,
                Surname = d.Surname,
                UserType = UserType.Doctor,
                DateOfBirth = d.DateOfBirth,
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
                    Id = d.CityZipCode,
                    Name = d.City.Name,
                    Country = new CountryMemento()
                    {
                        Id = d.City.CountryId,
                        Name = d.City.Country.Name
                    }
                },
                Gender = (d.Gender == 0) ? Gender.Male : Gender.Female
            }));
        }
    }
}
