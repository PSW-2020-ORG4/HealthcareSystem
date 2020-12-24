using GraphicalEditorServer.DTO;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class DrugMapper
    {
        public static DrugDTO DrugToDrugDTO(Drug drug)
        {
            return new DrugDTO(DrugTypeMapper.DrugTypeTODrugTypeDTO(drug.DrugType), drug.Name, drug.Quantity, drug.ExpirationDate, drug.Producer);
        }

        public static Drug DrugDTOToDrug(DrugDTO drugDTO) {
            return new Drug(DrugTypeMapper.DrugTypeDTOToDrugType(drugDTO.DrugTypeDTO), drugDTO.Name, drugDTO.Quantity, drugDTO.ExpirationDate, drugDTO.Producer);
        }
    }
}
