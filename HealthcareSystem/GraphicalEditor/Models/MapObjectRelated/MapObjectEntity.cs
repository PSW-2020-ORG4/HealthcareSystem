using GraphicalEditor.Constants;
using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectEntity : Entity
    {
        public MapObjectType MapObjectType { get; set; }
        public String Description { get; set; }

       
        public MapObjectEntity(MapObjectTypes mapObjectType, string description)
            :base()
        {
            MapObjectType = new MapObjectType(mapObjectType);
            Description = description;
        }

        public SolidColorBrush getColor()
        {
            return MapObjectType.getColor();
        }

        public void setStrokeAndStrokeThickness(Rectangle reactangle)
        {
            if (MapObjectType.TypeOfMapObject != MapObjectTypes.ROAD)
            {
                reactangle.Stroke = Brushes.Black;
                reactangle.StrokeThickness = AllConstants.RECTANGLE_STROKE_THICKNESS;
            }
        }

    }
}
