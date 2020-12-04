using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO
{
    public class EquipmentWithRoomDTO
    {
        public int IdEquipment { get; set; }
        public int RoomNumber { get; set; }
        public int Quantity { get; set; }
        public string EquipmentName { get; set; }
        public bool IsEquipmentConsumable { get; set; }
    }
}
