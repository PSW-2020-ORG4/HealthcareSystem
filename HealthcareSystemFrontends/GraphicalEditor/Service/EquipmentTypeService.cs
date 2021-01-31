
using GraphicalEditor.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicalEditor.DTO;

namespace GraphicalEditor.Service
{
    class EquipmentTypeService: GenericHTTPService
    {       
        public List<EquipmentTypeDTO> GetEquipmentTypes()
        {
            return (List<EquipmentTypeDTO>)HTTPGetRequest<EquipmentTypeDTO>("equipmentType");
        }

        public EquipmentTypeDTO GetExaminationById(int equipmentTypeId)
        {
            return (EquipmentTypeDTO)HTTPGetSingleItemRequest<EquipmentTypeDTO>("equipmentType/" + equipmentTypeId);
        }
    }
}
