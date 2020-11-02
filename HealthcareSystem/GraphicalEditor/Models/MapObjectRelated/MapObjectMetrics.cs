using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectMetrics
    {
        public double WidthOfMapObject { get; set; }
        public double HeightOfMapObject { get; set; }
        public double XOfCanvas { get; set; }
        public double YOfCanvas { get; set; }

        public MapObjectMetrics(double xOfCanvas, double yOfCanvas, double widthOfMapObject, double heightOfMapObject)
        {
            XOfCanvas = xOfCanvas;
            YOfCanvas = yOfCanvas;
            WidthOfMapObject = widthOfMapObject;
            HeightOfMapObject = heightOfMapObject;
        }

    }
}
