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
        private Building _building;

        public Room(MapObjectTypes mapObjectType, String description, MapObjectDepartment department, int floor,MapObject building)
            : base(new MapObjectType(mapObjectType), "Test")
        {
            Department = department;
            Floor = floor;
            Building = (Building)building.MapObjectEntity;
        }

        public MapObjectDepartment Department { get => _department; set => _department = value; }
        public int Floor { get => _floor; set => _floor = value; }
        internal Building Building { get => _building; set => _building = value; }
    }
}
