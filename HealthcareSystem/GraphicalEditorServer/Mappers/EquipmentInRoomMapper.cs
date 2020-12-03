using GraphicalEditorServer.DTO;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class EquipmentWithRoomMapper
    {
        public static EquipmentWithRoomDTO EquipmentToEquipmentWithRoomDTO(Equipment e, EquipmentInRooms equipmentInRoom)
        {
            EquipmentWithRoomDTO equipmentWithRoomDTO = new EquipmentWithRoomDTO();
            equipmentWithRoomDTO.IdEquipment = e.Id;
            equipmentWithRoomDTO.RoomNumber = equipmentInRoom.RoomNumber;
            equipmentWithRoomDTO.Quantity = e.Id;
            equipmentWithRoomDTO.EquipmentName = e.Type.Name;
            equipmentWithRoomDTO.IsEquipmentConsumable = e.Type.IsConsumable;

            return equipmentWithRoomDTO;
        }
    }
}
