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


        public Room(MapObjectTypes mapObjectType, String description, MapObjectDepartments department, MapObject building, int floor)
            : base(mapObjectType, description)
        {
            Department = new MapObjectDepartment(department);
            Floor = floor;
            BuildingId = building.MapObjectEntity.Id;

            FormatObjectDescription(Description);
        }

        public Room(MapObjectTypes mapObjectType, MapObjectDepartments department, MapObject building, int floor)
             : base(mapObjectType, "")
        {
            Department = new MapObjectDepartment(department);
            Floor = floor;
            BuildingId = building.MapObjectEntity.Id;

            FormatObjectDescription(Description);
        }


        [JsonConstructor]
        public Room(MapObjectType mapObjectType, String description, MapObjectDepartment department, long buildingId, int floor)
            : base(mapObjectType.TypeOfMapObject, description)
        {
            Department = department;
            Floor = floor;
            BuildingId = buildingId;
        }

        public override void FormatObjectDescription(string description)
        {
            if (String.IsNullOrEmpty(description))
            {
                Description = MapObjectType.ObjectTypeFullName + " " + Id + " se nalazi u: Zgrada " + BuildingId + ", Departman: " + Department.DepartmentFullName + ", Sprat: " + Floor + ". sprat";
            }
        }
    }
}
