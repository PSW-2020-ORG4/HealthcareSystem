using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Model.Users;
using PatientWebApp.DTOs;

namespace PatientWebApp.Mappers
{
    public class DoctorSpecialtyMapper
    {
        public static DoctorSpecialty DoctorSpecialtyDTOToDoctorSpecialty(DoctorSpecialtyDTO doctorSpecialtyDTO)
        {
            DoctorSpecialty doctorSpecialty = new DoctorSpecialty();
            doctorSpecialty.DoctorJmbg = doctorSpecialtyDTO.DoctorJmbg;
            doctorSpecialty.SpecialtyId = doctorSpecialtyDTO.IdSpecialty;
            return doctorSpecialty;
        }

        public static DoctorSpecialtyDTO DoctorSpecialtyToDoctorSpecialtyDTO(DoctorSpecialty doctorSpecialty)
        {
            DoctorSpecialtyDTO doctorSpecialtyDTO = new DoctorSpecialtyDTO();
            doctorSpecialtyDTO.DoctorJmbg = doctorSpecialty.DoctorJmbg;
            doctorSpecialtyDTO.IdSpecialty = doctorSpecialty.SpecialtyId;
            return doctorSpecialtyDTO;
        }
    }
}
