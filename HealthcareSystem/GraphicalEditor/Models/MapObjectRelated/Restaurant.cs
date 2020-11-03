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
        public Restaurant(String description = "restaurant")
          : base(new MapObjectType(MapObjectTypes.RESTAURANT), description)
        {
        }
    }
}
