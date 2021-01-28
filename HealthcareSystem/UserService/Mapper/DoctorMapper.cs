using UserService.DTO;
using UserService.Model;

namespace UserService.Mapper
{
    public static class DoctorMapper
    {
        public static DoctorDTO ToDoctorDTO(this DoctorAccount doctor)
        {
            var memento = doctor.GetDoctorMemento();
            return new DoctorDTO()
            {
                Name = memento.Name,
                Surname = memento.Surname,
                Jmbg = memento.Jmbg
            };
        }
    }
}
