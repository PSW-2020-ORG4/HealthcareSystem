using GraphicalEditor.Constants;
using GraphicalEditor.Models.MapObjectRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.Models
{
    public class MapObject
    {
        private Rectangle _rectangle;
        private MapObjectDoor _mapObjectDoor;
        private MapObjectEntity _mapObjectEntity;
        private MapObjectMetrics _mapObjectMetrics;

        public MapObject(MapObjectEntity mapObjectEntity, MapObjectMetrics mapObjectMetrics, MapObjectDoor mapObjectDoor)
        {
            MapObjectEntity = mapObjectEntity;
            MapObjectMetrics = mapObjectMetrics;

            mapObjectDoor.MapObjectMetrics = this.MapObjectMetrics;
            MapObjectDoor = mapObjectDoor;

            RectangleInitialization();
        }

        private void RectangleInitialization()
        {
            Rectangle = new Rectangle();
            Rectangle.Fill = MapObjectEntity.getColor();
            Rectangle.Height = MapObjectMetrics.Height;
            Rectangle.Width = MapObjectMetrics.Width;

            Rectangle.Stroke = Brushes.Black;
            Rectangle.StrokeThickness = AllConstants.RECTANGLE_STROKE_THICKNESS;

            Rectangle.SetValue(Canvas.LeftProperty, MapObjectMetrics.X);
            Rectangle.SetValue(Canvas.TopProperty, MapObjectMetrics.Y);
        }

        public void AddToCanvas(Canvas canvas)
        {
            canvas.Children.Add(Rectangle);
            if(!_mapObjectEntity.GetMapObjectTypes.Equals(MapObjectTypes.PARKING))
              canvas.Children.Add(MapObjectDoor.GetDoor());
        }

        public Rectangle Rectangle { get => _rectangle; set => _rectangle = value; }
        public MapObjectEntity MapObjectEntity { get => _mapObjectEntity; set => _mapObjectEntity = value; }
        public MapObjectMetrics MapObjectMetrics { get => _mapObjectMetrics; set => _mapObjectMetrics = value; }
        public MapObjectDoor MapObjectDoor { get => _mapObjectDoor; set => _mapObjectDoor = value; }
    }
}
