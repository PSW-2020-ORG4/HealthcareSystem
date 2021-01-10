using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO
{
    public class EquipmentWithRoomDTO
    {
        public int IdEquipment { get; set; }
        public int RoomNumber { get; set; }
        public int Quantity { get; set; }
        public string EquipmentName { get; set; }
        public bool IsEquipmentConsumable { get; set; }

        public EquipmentWithRoomDTO() {}

        public EquipmentWithRoomDTO(int idEquipment, int roomNumber, int quantity, string equipmentName)
        {
            this.IdEquipment = idEquipment;
            this.RoomNumber = roomNumber;
            this.Quantity = quantity;
            this.EquipmentName = equipmentName;
        }
    }
}
