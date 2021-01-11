using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Model.Manager
{
   public class EquipmentTransfer
    {
        public int RoomNumber { get; set; }
        public DateTime DateAndTimeOfTransfer { get; set; }

        public int Id { get; set; }

        public EquipmentTransfer() { }

        public EquipmentTransfer(int roomNumber, DateTime dateAndTimeOfTransfer)
        {
            RoomNumber = roomNumber;
            DateAndTimeOfTransfer = dateAndTimeOfTransfer;
        }
    }
}
