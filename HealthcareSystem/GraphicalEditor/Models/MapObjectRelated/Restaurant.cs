using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraphicalEditor.Models.MapObjectRelated
{
    class Restaurant : MapObjectEntity
    {
        public Restaurant(String description)
          : base(new MapObjectType(MapObjectTypes.RESTAURANT), description)
        {
            FormatObjectDescription(Description);
        }

        public Restaurant()
          : base(new MapObjectType(MapObjectTypes.RESTAURANT), "")
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
