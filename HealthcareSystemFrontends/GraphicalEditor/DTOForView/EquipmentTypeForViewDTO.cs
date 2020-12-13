using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicalEditor.DTO;

namespace GraphicalEditor.DTOForView
{
    public class EquipmentTypeForViewDTO
    {
        public EquipmentTypeDTO EquipmentType { get; set; }
        public bool IsSelected { get; set; }


        public EquipmentTypeForViewDTO()
        {
        }

        public EquipmentTypeForViewDTO(EquipmentTypeDTO equipementType, bool isSelected)
        {
            EquipmentType = equipementType;
            IsSelected = isSelected;
        }
    }
}
