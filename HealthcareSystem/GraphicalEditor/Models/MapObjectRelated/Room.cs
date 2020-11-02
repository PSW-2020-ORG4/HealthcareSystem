using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class Room : MapObjectEntity
    {
        public MapObjectDepartment Department { get; set; }
        public int Floor { get; set; }
        public Building Building { get; set; }

        public Room(MapObjectTypes mapObjectType, String description, MapObjectDepartment department, MapObject building, int floor)
            : base(new MapObjectType(mapObjectType), description)
        {
            Department = department;
            Floor = floor;
            Building = (Building)building.MapObjectEntity;
        }

    }
}
