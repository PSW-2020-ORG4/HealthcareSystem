using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO
{
   public class DrugWithRoomDTO
    {
        public int DrugId { get; set; }
        public int RoomNumber { get; set; }
        public int Quantity { get; set; }
        public string DrugName { get; set; }
    }
}
