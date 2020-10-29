using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectBasics
    {
        private MapObjectType _mapObjectType;
        private String _description;
        private MapObjectDepartment _department;
        private int _floor;

        public MapObjectBasics(MapObjectType mapObjectType, String description, MapObjectDepartment department, int floor)
        {
            MapObjectType = mapObjectType;
            Description = description;
            Department = department;
            Floor = floor;
        }

        public MapObjectType MapObjectType { get => _mapObjectType; set => _mapObjectType = value; }
        public string Description { get => _description; set => _description = value; }
        public MapObjectDepartment Department { get => _department; set => _department = value; }
        public int Floor { get => _floor; set => _floor = value; }
    }
}
