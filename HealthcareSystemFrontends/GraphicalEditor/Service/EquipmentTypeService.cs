using Backend.Model.Manager;
using GraphicalEditor.Models.Equipments;
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
        public string AddEquipmentType(EquipmentType equipmentType)
        {
            IRestResponse response = AddHTTPPostRequest("equipmentType", equipmentType);
            return response.Content;
        }

        public List<EquipmentTypeDTO> GetEquipmentTypes()
        {
            return (List<EquipmentTypeDTO>)HTTPGetRequest<EquipmentTypeDTO>("equipmentType");
        }
    }
}
