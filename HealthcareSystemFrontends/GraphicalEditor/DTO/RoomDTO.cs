using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.DTO
{
    public class RoomDTO
    {
        public long Id { get; set; }
        public TypeOfMapObject Usage { get; set; }

        public RoomDTO() {}

        public RoomDTO(long id, TypeOfMapObject usage)
        {
            this.Id = id;
            this.Usage = usage;
        }
    }
}
