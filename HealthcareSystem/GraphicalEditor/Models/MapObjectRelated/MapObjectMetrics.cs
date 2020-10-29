using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectMetrics
    {
        private double _width;
        private double _height;
        private double _x;
        private double _y;

        public MapObjectMetrics(double x, double y, double width, double height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public double Width { get => _width; set => _width = value; }
        public double Height { get => _height; set => _height = value; }
        public double X { get => _x; set => _x = value; }
        public double Y { get => _y; set => _y = value; }
    }
}
