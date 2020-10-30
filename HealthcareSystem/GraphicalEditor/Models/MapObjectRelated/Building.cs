using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
    class Building : MapObjectEntity
    {
        private int _numOfFloors;

        public Building(String description, int numOfFloors)
            : base(new MapObjectType(MapObjectTypes.BUILDING), "Building 1")
        {
            NumOfFloors = numOfFloors;
        }

        public int NumOfFloors { get => _numOfFloors; set => _numOfFloors = value; }
    }
}
