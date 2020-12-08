using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Users;
using PatientWebApp.DTOs;

namespace PatientWebApp.Mappers
{
    public class SpecialtyMapper
    {
        public static Specialty SpecialtyDTOToSpecialty(SpecialtyDTO specialtyDTO)
        {
            Specialty specialty = new Specialty();
            specialty.Id = specialtyDTO.Id;
            specialty.Name = specialtyDTO.Name;
            return specialty;
        }

        public static SpecialtyDTO SpecialtyToSpecialtyDTO(Specialty specialty)
        {
            SpecialtyDTO specialtyDTO = new SpecialtyDTO();
            specialtyDTO.Id = specialty.Id;
            specialtyDTO.Name = specialty.Name;
            return specialtyDTO;
        }
    }
}
