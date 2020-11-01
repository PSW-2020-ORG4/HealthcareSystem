using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectEntity 
    {
        public MapObjectType MapObjectType { get; set; }
        public String Description { get; set; }

        public MapObjectEntity(MapObjectType mapObjectType, string description)
        {
            MapObjectType = mapObjectType;
            Description = description;
        }

        public SolidColorBrush getColor()
        {
            return MapObjectType.getColor();
        }
        
    }
}
