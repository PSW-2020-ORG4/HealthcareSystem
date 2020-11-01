using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
   public class Building : MapObjectEntity
    {
        public int NumOfFloors { get; set; }

        public Building(String description, int numOfFloors)
            : base(new MapObjectType(MapObjectTypes.BUILDING), "Building 1")
        {
            NumOfFloors = numOfFloors;
        }

    }
}
