using System;

namespace GraphicalEditorServer.DTO.EventSourcingDTO
{
    public class RoomSelectionEventDTO
    {
        public String Username { get; set; }
        public int RoomNumber { get; set; }

        public RoomSelectionEventDTO() { }

        public RoomSelectionEventDTO(string username, int roomNumber)
        {
            Username = username;
            RoomNumber = roomNumber;
        }
    }
}
