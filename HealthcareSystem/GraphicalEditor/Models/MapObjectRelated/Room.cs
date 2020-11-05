using GraphicalEditor.Enumerations;
using Newtonsoft.Json;
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
        public long BuildingId { get; set; }

        public Room(MapObjectTypes mapObjectType, String description, MapObjectDepartment department, MapObject building, int floor)
            : base(mapObjectType, description)
        {
            Department = department;
            Floor = floor;
            BuildingId = building.MapObjectEntity.Id;
        }


        [JsonConstructor]
        public Room(MapObjectType mapObjectType, String description, MapObjectDepartment department, long buildingId, int floor)
            : base(mapObjectType.TypeOfMapObject, description)
        {
            Department = department;
            Floor = floor;
            BuildingId = buildingId;
        }

    }
}
