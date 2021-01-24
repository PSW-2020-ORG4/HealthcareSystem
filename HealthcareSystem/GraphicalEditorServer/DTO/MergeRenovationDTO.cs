using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO
{
    public class MergeRenovationDTO
    {
        public BaseRenovationDTO baseRenovation { get; set; }
        public int SecondRoomId { get; set; }
        public string NewRoomDescription { get; set; }
        public TypeOfMapObject RoomType { get; set; }

        public MergeRenovationDTO()
        {
        }

        public MergeRenovationDTO(BaseRenovationDTO baseRenovation, int secondRoomId, string newRoomDescription, TypeOfMapObject roomType)
        {
            this.baseRenovation = baseRenovation;
            SecondRoomId = secondRoomId;
            NewRoomDescription = newRoomDescription;
            RoomType = roomType;
        }
    }
}
