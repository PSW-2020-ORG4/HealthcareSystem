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
        private MapObjectType _mapObjectType;
        private String _description;

        public MapObjectEntity(MapObjectType mapObjectType, string description)
        {
            _mapObjectType = mapObjectType;
            _description = description;
        }

        public SolidColorBrush getColor()
        {
            return _mapObjectType.getColor();
        }
    }
}
