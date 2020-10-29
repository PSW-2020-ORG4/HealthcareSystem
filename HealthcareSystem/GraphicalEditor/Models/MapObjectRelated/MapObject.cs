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
        private MapObjectBasics _mapObjectBasics;
        private MapObjectMetrics _mapObjectMetrics;

        public readonly static double RECTANGLE_STROKE_THICKNESS = 3;

        public MapObject(MapObjectBasics mapObjectBasics, MapObjectMetrics mapObjectMetrics)
        {
            this.MapObjectBasics = mapObjectBasics;
            this.MapObjectMetrics = mapObjectMetrics;

            RectangleInitialization();
        }

        private void RectangleInitialization()
        {
            Rectangle = new Rectangle();
            Rectangle.Fill = MapObjectBasics.MapObjectType.getColor();
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
        public MapObjectBasics MapObjectBasics { get => _mapObjectBasics; set => _mapObjectBasics = value; }
        public MapObjectMetrics MapObjectMetrics { get => _mapObjectMetrics; set => _mapObjectMetrics = value; }
    }
}
