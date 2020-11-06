using System;
using GraphicalEditor.Enumerations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class Road : MapObjectEntity
    {
        public Road(String description = "road")
                   : base(MapObjectTypes.ROAD, description)
        {
        }

    }
}
