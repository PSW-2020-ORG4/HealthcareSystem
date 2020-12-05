using Backend.Model.Manager;
using GraphicalEditorServer.DTO;
using Model.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.Mappers
{
    public class DrugWithRoomMapper
    {
        public static DrugWithRoomDTO DrugToDrugWithRoomDTO(Drug drug, DrugInRoom drugInRoom)
        {
            DrugWithRoomDTO drugWithRoomDTO = new DrugWithRoomDTO();
            drugWithRoomDTO.DrugId = drug.Id;
            drugWithRoomDTO.RoomNumber = drugInRoom.RoomNumber;
            drugWithRoomDTO.Quantity = drug.Quantity;
            drugWithRoomDTO.DrugName = drug.Name;

            return drugWithRoomDTO;
        }
    }
}
