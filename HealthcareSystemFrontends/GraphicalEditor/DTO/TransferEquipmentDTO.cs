using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO
{
   public class TransferEquipmentDTO
    {
        public int EquipmentTypeId { get; set; }
        public int StartingRoomNumber { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAndTimeOfTransfer { get; set; }
        public int DestinationRoomNumber { get; set; }

        public TransferEquipmentDTO() { }

        public TransferEquipmentDTO(int equipmentTypeId, int startingRoomNumber, int quantity, DateTime dateAndTimeOfTransfer, int destinationRoomNumber)
        {
            EquipmentTypeId = equipmentTypeId;
            StartingRoomNumber = startingRoomNumber;
            Quantity = quantity;
            DateAndTimeOfTransfer = dateAndTimeOfTransfer;
            DestinationRoomNumber = destinationRoomNumber;
        }
    }
}
