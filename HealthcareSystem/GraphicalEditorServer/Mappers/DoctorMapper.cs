using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphicalEditorServer.DTO;
using Model.Users;

namespace GraphicalEditorServer.Mappers
{
    public class DoctorMapper
    {
        public static DoctorDTO DoctorToDoctorDTO(Doctor doctor)
        {
            DoctorDTO doctorDTO = new DoctorDTO();
            doctorDTO.Name = doctor.Name;
            doctorDTO.Surname = doctor.Surname;
            doctorDTO.Jmbg = doctor.Jmbg;
            return doctorDTO;
        }
    }

}
