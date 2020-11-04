﻿using GraphicalEditor.Constants;
using GraphicalEditor.Enumerations;
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

        public MapObject(MapObjectEntity mapObjectEntity, MapObjectMetrics mapObjectMetrics, MapObjectDoor mapObjectDoor)
        {
            MapObjectEntity = mapObjectEntity;
            MapObjectMetrics = mapObjectMetrics;

            MapObjectDoor = mapObjectDoor;
            MapObjectDoor.MapObjectMetrics = mapObjectMetrics;

            RectangleInitialization();
        }

        private void RectangleInitialization()
        {
            Rectangle = new Rectangle();
            Rectangle.Fill = MapObjectEntity.getColor();
            Rectangle.Height = MapObjectMetrics.HeightOfMapObject;
            Rectangle.Width = MapObjectMetrics.WidthOfMapObject;

            Rectangle.Stroke = Brushes.Black;
            Rectangle.StrokeThickness = AllConstants.RECTANGLE_STROKE_THICKNESS;
        }

        public void AddToCanvas(Canvas canvas)
        {
            canvas.Children.Add(Rectangle);
            Canvas.SetLeft(Rectangle, MapObjectMetrics.XOfCanvas);
            Canvas.SetTop(Rectangle, MapObjectMetrics.YOfCanvas);

            canvas.Children.Add(MapObjectDoor.GetDoor());
        }
    }
}
