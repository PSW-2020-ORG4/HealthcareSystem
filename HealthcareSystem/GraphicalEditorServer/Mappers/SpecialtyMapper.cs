using Backend.Model.Users;
using GraphicalEditorServer.DTO;

namespace GraphicalEditorServer.Mappers
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

