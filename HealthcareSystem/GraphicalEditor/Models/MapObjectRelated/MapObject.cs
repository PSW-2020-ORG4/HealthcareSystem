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
        private MapObjectEntity _mapObjectEntity;
        private MapObjectMetrics _mapObjectMetrics;

        public readonly static double RECTANGLE_STROKE_THICKNESS = 3;

        public MapObject(MapObjectEntity mapObjectEntity, MapObjectMetrics mapObjectMetrics)
        {
            this.MapObjectEntity = mapObjectEntity;
            this.MapObjectMetrics = mapObjectMetrics;

            RectangleInitialization();
        }

        private void RectangleInitialization()
        {
            Rectangle = new Rectangle();
            Rectangle.Fill = MapObjectEntity.getColor();
            Rectangle.Height = MapObjectMetrics.Height;
            Rectangle.Width = MapObjectMetrics.Width;

            Rectangle.Stroke = Brushes.Black;
            Rectangle.StrokeThickness = RECTANGLE_STROKE_THICKNESS;

            Rectangle.SetValue(Canvas.LeftProperty, MapObjectMetrics.X);
            Rectangle.SetValue(Canvas.TopProperty, MapObjectMetrics.Y);
        }

        public void AddToCanvas(Canvas canvas)
        {
            canvas.Children.Add(this.Rectangle);
        }

        public Rectangle Rectangle { get => _rectangle; set => _rectangle = value; }
        public MapObjectEntity MapObjectEntity { get => _mapObjectEntity; set => _mapObjectEntity = value; }
        public MapObjectMetrics MapObjectMetrics { get => _mapObjectMetrics; set => _mapObjectMetrics = value; }
    }
}
