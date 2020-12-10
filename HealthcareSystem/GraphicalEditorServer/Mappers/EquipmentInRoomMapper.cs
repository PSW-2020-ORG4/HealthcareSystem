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
        public static List<EquipmentWithRoomDTO> EquipmentToEquipmentWithRoomDTO(Equipment e, List<EquipmentInRooms> equipmentInRooms)
        {
            List<EquipmentWithRoomDTO> equipmentWithRoomDTOs = new List<EquipmentWithRoomDTO>();
            foreach (EquipmentInRooms eqInRoom in equipmentInRooms)
            {
                EquipmentWithRoomDTO equipmentWithRoomDTO = new EquipmentWithRoomDTO();
                equipmentWithRoomDTO.IdEquipment = e.Id;
                equipmentWithRoomDTO.RoomNumber = eqInRoom.RoomNumber;
                equipmentWithRoomDTO.Quantity = e.Quantity;
                equipmentWithRoomDTO.EquipmentName = e.Type.Name;
                equipmentWithRoomDTO.IsEquipmentConsumable = e.Type.IsConsumable;
                equipmentWithRoomDTOs.Add(equipmentWithRoomDTO);
            }
            
            return equipmentWithRoomDTOs;
        }
    }
}
