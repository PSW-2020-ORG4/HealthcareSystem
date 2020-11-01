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
        public Rectangle Rectangle { get; set; }
        public MapObjectDoor MapObjectDoor { get; set; }
        public MapObjectEntity MapObjectEntity { get; set; }
        public MapObjectMetrics MapObjectMetrics { get; set; }
        public long Id { get; set; }

        public MapObject(MapObjectEntity mapObjectEntity, MapObjectMetrics mapObjectMetrics, MapObjectDoor mapObjectDoor,List<MapObject> mapObjects)
        {
            MapObjectEntity = mapObjectEntity;
            MapObjectMetrics = mapObjectMetrics;

            MapObjectDoor = mapObjectDoor;
            MapObjectDoor.MapObjectMetrics = mapObjectMetrics;
            Id = mapObjects.Count() == 0 ? 0 : mapObjects.Max(entity => entity.Id) + 1;
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
            canvas.Children.Add(MapObjectDoor.GetDoor());
        }
    }
}
