using Model.Users;
using PatientWebApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientWebApp.Mappers
{
    public class DoctorMapper
    {
        public static DoctorDTO DoctorToDoctorTDO(Doctor doctor)
        {
            DoctorDTO doctorDTO = new DoctorDTO();
            doctorDTO.Name = doctor.Name;
            doctorDTO.Surname = doctor.Surname;
            doctorDTO.Jmbg = doctor.Jmbg;
            return doctorDTO;
        }
    }
}
