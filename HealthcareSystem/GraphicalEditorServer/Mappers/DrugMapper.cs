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
            DrugDTO drugDTO = new DrugDTO();
            drugDTO.Name = drug.Name;
            drugDTO.Quantity = drug.Quantity;
            drugDTO.ExpirationDate = drug.ExpirationDate;
            drugDTO.Producer = drug.Producer;
            drugDTO.DrugTypeDTO = DrugTypeMapper.DrugTypeTODrugTypeDTO(drug.DrugType);
            return drugDTO;
        }

        public static Drug DrugDTOToDrug(DrugDTO drugDTO) {
            return new Drug(DrugTypeMapper.DrugTypeDTOToDrugType(drugDTO.DrugTypeDTO), drugDTO.Name, drugDTO.Quantity, drugDTO.ExpirationDate, drugDTO.Producer);
        }
    }
}
