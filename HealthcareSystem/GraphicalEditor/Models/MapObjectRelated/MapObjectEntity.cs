using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectEntity : Entity
    {
        public MapObjectType MapObjectType { get; set; }
        public String Description { get; set; }

       
        public MapObjectEntity(MapObjectType mapObjectType, string description)
            :base()
        {
            MapObjectType = mapObjectType;
            Description = description;
        }

         public MapObjectEntity(string description)
        {
            _mapObjectType = new MapObjectType(MapObjectTypes.PARKING);
            _description = description;
        }

        public SolidColorBrush getColor()
        {
            return MapObjectType.getColor();
        }
        
    }
}
