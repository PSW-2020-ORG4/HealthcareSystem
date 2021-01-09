using GraphicalEditor.DTO;
using System;
using System.Collections.Generic;

namespace GraphicalEditor.Service
{
    public class EquipementService : GenericHTTPService
    {
        public List<EquipmentDTO> GetEquipmentByRoomNumber(int roomNumber)
        {
            return (List<EquipmentDTO>)HTTPGetRequest<EquipmentDTO>("equipment/byRoomNumber/ " + roomNumber);
        }

        public List<EquipmentWithRoomDTO> GetEquipmentWithRoomForSearchTerm(String searchTerm)
        {
            return (List<EquipmentWithRoomDTO>) HTTPGetRequest<EquipmentWithRoomDTO>("equipment/search?term=" + searchTerm);
        }
    }
}
