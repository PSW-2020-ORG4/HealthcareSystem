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
        private MapObjectDepartment _department;
        private int _floor;

        public Room(MapObjectTypes mapObjectType, String description, MapObjectDepartment department, int floor)
            : base(new MapObjectType(mapObjectType), "Test")
        {
            Department = department;
            Floor = floor;
        }

        public MapObjectDepartment Department { get => _department; set => _department = value; }
        public int Floor { get => _floor; set => _floor = value; }
    }
}
