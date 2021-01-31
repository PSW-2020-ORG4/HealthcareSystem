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

        public static Doctor DoctorDTOToDoctor(DoctorDTO doctorDTO)
        {
            Doctor doctor = new Doctor();
            doctor.Name = doctorDTO.Name;
            doctor.Surname = doctorDTO.Surname;
            doctor.Jmbg = doctorDTO.Jmbg;
            return doctor;
        }
    }

}
