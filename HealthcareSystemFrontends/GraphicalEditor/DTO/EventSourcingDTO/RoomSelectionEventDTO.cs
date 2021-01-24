using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO.EventSourcingDTO
{
    class RoomSelectionEventDTO
    {
        public String Username { get; set; }
        public int RoomNumber { get; set; }

        public RoomSelectionEventDTO() {}

        public RoomSelectionEventDTO(string username, int roomNumber)
        {
            Username = username;
            RoomNumber = roomNumber;
        }
    }
}
