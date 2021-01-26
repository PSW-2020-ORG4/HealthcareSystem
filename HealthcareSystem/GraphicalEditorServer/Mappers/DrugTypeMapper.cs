using GraphicalEditorServer.DTO;
using Model.Manager;

namespace GraphicalEditorServer.Mappers
{
    public class DrugTypeMapper
    {
        public static DrugTypeDTO DrugTypeTODrugTypeDTO(DrugType drugType)
        {
            return new DrugTypeDTO(drugType.Id, drugType.Type, drugType.Purpose);
        }

        public static DrugType DrugTypeDTOToDrugType(DrugTypeDTO drugTypeDTO)
        {
            return new DrugType(drugTypeDTO.Type, drugTypeDTO.Purpose);
        }
    }
}
