using GraphicalEditorServer.DTO;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class DrugTypeMapper
    {
        public static DrugTypeDTO DrugTypeTODrugTypeDTO(DrugType drugType) {
            return new DrugTypeDTO(drugType.Id,drugType.Type,drugType.Purpose);
        } 

        public static DrugType DrugTypeDTOToDrugType(DrugTypeDTO drugTypeDTO) {
            return new DrugType(drugTypeDTO.Type, drugTypeDTO.Purpose);
        }
    }
}
