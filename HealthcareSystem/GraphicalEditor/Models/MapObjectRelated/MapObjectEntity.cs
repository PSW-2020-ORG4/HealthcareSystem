using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectEntity : IdGenerator
    {
        private MapObjectType _mapObjectType;
        private String _description;

        public MapObjectEntity(string description = "")
        {
            _mapObjectType = new MapObjectType(MapObjectTypes.PARKING);
            _description = description;
        }
        public MapObjectEntity(MapObjectType mapObjectType, string description)
        {
            MapObjectType = mapObjectType;
            Description = description;
        }

        public SolidColorBrush getColor()
        {
            return MapObjectType.getColor();
        }

        public MapObjectType MapObjectType { get => _mapObjectType; set => _mapObjectType = value; }

        public string Description { get => _description; set => _description = value; }
        
    }
}
