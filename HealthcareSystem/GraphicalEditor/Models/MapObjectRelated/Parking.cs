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
        public Parking(String description = "parking")
            : base(MapObjectTypes.PARKING, description)
        {
        }
    }
}
