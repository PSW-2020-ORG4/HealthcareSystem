using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEditor.Models.Drugs
{
   public class DrugInRoom
    {
        public int DrugId { get; set; }
        public int Quantity { get; set; }
        public int RoomNumber { get; set; }

        public DrugInRoom() { }
        public DrugInRoom(int DrugId, int Quantity, int RoomNumber)
        {
            this.DrugId = DrugId;
            this.Quantity = Quantity;
            this.RoomNumber = RoomNumber;
        }
        public DrugInRoom(DrugInRoom drugInRoom)
        {
            this.DrugId = drugInRoom.DrugId;
            this.Quantity = drugInRoom.Quantity;
            this.RoomNumber = drugInRoom.RoomNumber;
        }
    }
}
