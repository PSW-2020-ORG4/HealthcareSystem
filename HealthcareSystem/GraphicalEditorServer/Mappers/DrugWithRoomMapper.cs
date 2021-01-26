using Backend.Model.Manager;
using GraphicalEditorServer.DTO;
using Model.Manager;
using System.Collections.Generic;

namespace GraphicalEditorServer.Mappers
{
    public class DrugWithRoomMapper
    {
        public static List<DrugWithRoomDTO> DrugToDrugWithRoomDTO(Drug drug, List<DrugInRoom> drugsInRooms)
        {
            List<DrugWithRoomDTO> drugWithRoomDTOs = new List<DrugWithRoomDTO>();
            foreach (DrugInRoom drugInRoom in drugsInRooms)
            {
                DrugWithRoomDTO drugWithRoomDTO = new DrugWithRoomDTO();
                drugWithRoomDTO.DrugId = drug.Id;
                drugWithRoomDTO.RoomNumber = drugInRoom.RoomNumber;
                drugWithRoomDTO.Quantity = drug.Quantity;
                drugWithRoomDTO.DrugName = drug.Name;
                drugWithRoomDTOs.Add(drugWithRoomDTO);
            }


            return drugWithRoomDTOs;
        }
    }
}
