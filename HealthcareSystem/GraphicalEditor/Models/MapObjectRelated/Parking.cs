using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
    class Parking : MapObjectEntity
    {
        public Parking(String description)
            : base(MapObjectTypes.PARKING, description)
        {
            FormatObjectDescription(Description);
        }

        public Parking()
           : base(MapObjectTypes.PARKING, "")
        {
            FormatObjectDescription(Description);
        }

        public override void FormatObjectDescription(string description)
        {
            if (String.IsNullOrEmpty(description))
            {
                Description = MapObjectType.ObjectTypeFullName;
            }
        }
    }
}
