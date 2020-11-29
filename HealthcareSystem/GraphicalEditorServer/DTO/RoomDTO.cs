using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicalEditorServer.DTO
{
    public class RoomDTO
    {
        public long Id { get; set; }
        public TypeOfMapObject Usage { get; set; }

    }
}
