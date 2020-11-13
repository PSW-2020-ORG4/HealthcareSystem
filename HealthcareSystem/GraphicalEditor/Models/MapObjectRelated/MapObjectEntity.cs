using GraphicalEditor.Constants;
using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

       
        public MapObjectEntity(TypeOfMapObject mapObjectType, string description = "")
            :base()
        {
            MapObjectType = new MapObjectType(mapObjectType);
            Description = description;
        }

        public virtual void FormatObjectDescription(string description)
        {
        }

        public SolidColorBrush ObjectEntityColor => MapObjectType.ObjectTypeColor;
        

        public void SetStrokeAndStrokeThickness(Rectangle reactangle)
        {
            if (MapObjectType.TypeOfMapObject != TypeOfMapObject.ROAD)
            {
                reactangle.Stroke = Brushes.Black;
                reactangle.StrokeThickness = AllConstants.RECTANGLE_STROKE_THICKNESS;
            }
        }

    }
}
