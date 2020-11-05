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

        public Room(MapObjectTypes mapObjectType, String description, MapObjectDepartments department, MapObject building, int floor)
            : base(new MapObjectType(mapObjectType), description)
        {
            
            Department = new MapObjectDepartment(department);
            Floor = floor;
            Building = (Building)building.MapObjectEntity;

            FormatObjectDescription(Description);
        }

        public Room(MapObjectTypes mapObjectType, MapObjectDepartments department, MapObject building, int floor)
             : base(new MapObjectType(mapObjectType), "")
        {

            Department = new MapObjectDepartment(department);
            Floor = floor;
            Building = (Building)building.MapObjectEntity;

            FormatObjectDescription(Description);
        }

        public override void FormatObjectDescription(string description)
        {
            if (String.IsNullOrEmpty(description))
            {
                Description = MapObjectType.ObjectTypeFullName + " " + Id + " se nalazi u: Zgrada " + Building.Id + ", Departman: " + Department.DepartmentFullName + ", Sprat: " + Floor + ". sprat";
            }
        }
    }
}
