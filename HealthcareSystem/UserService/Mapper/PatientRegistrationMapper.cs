using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.DTO;
using UserService.Model.Memento;

namespace UserService.Mapper
{
    public static class PatientRegistrationMapper
    {
        public static PatientAccountMemento ToPatientAccountMemento(this PatientRegistrationDTO dto)
        {
            return new PatientAccountMemento()
            {
                Jmbg = dto.Jmbg,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                Gender = dto.Gender,
                HomeAddress = dto.HomeAddress,
                ImageName = dto.ImageName,
                IsActivated = false,
                IsBlocked = false,
                Name = dto.Name,
                Password = dto.Password,
                Phone = dto.Phone,
                Surname = dto.Surname
            };
        }
    }
}
