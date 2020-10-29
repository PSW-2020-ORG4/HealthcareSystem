using GraphicalEditor.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicalEditor.Models.MapObjectRelated
{
    public class MapObjectDoor
    {
        private Rectangle _rectangle;
        private MapObjectMetrics _mapObjectMetrics;
        private MapObjectDoorOrientation _mapObjectDoorOrientation;
        private double _XShift;
        private double _YShift;

        private readonly double DOOR_WIDTH = 45;
        private readonly double DOOR_HEIGHT = 10;

        public MapObjectDoor(MapObjectDoorOrientation mapObjectDoorOrientations, double XShift = 0, double YShift = 0)
        {
            MapObjectDoorOrientation = mapObjectDoorOrientations;
            this.XShift = XShift;
            this.YShift = YShift;
        }

        public Rectangle GetDoor()
        {
            Rectangle = new Rectangle();
            Rectangle.Fill = Brushes.DarkGreen;
            Rectangle.Width = GetDoorWidth();
            Rectangle.Height = GetDoorHeight();

            Rectangle.SetValue(Canvas.LeftProperty, CalculateDoorX());
            Rectangle.SetValue(Canvas.TopProperty, CalculateDoorY());

            return Rectangle;
        }

        // Rotacija vrata - zameni sirinu i visinu ukoliko vrata trebaju da se postave vertikalno ( horizontalno su po default)
        private double GetDoorWidth()
        {
            if (MapObjectDoorOrientation == MapObjectDoorOrientation.TOP || MapObjectDoorOrientation == MapObjectDoorOrientation.BOTTOM)
                return DOOR_WIDTH;
            else return DOOR_HEIGHT;
        }

        // Rotacija vrata - zameni sirinu i visinu ukoliko vrata trebaju da se postave vertikalno (horizontalno su po default)
        private double GetDoorHeight()
        {
            if (MapObjectDoorOrientation == MapObjectDoorOrientation.TOP || MapObjectDoorOrientation == MapObjectDoorOrientation.BOTTOM)
                return DOOR_HEIGHT;
            else return DOOR_WIDTH;
        }

        private double CalculateDoorX()
        {
            switch (MapObjectDoorOrientation)
            {
                case MapObjectDoorOrientation.LEFT:
                    return MapObjectMetrics.X - GetDoorWidth() + MapObject.RECTANGLE_STROKE_THICKNESS;
                case MapObjectDoorOrientation.RIGHT:
                    return MapObjectMetrics.X + MapObjectMetrics.Width - MapObject.RECTANGLE_STROKE_THICKNESS;
                default:
                    return CalculateXShifted();
            }
        }

        private double CalculateDoorY()
        {
            switch (MapObjectDoorOrientation)
            {
                case MapObjectDoorOrientation.TOP:
                    return MapObjectMetrics.Y - GetDoorHeight() + MapObject.RECTANGLE_STROKE_THICKNESS;
                case MapObjectDoorOrientation.BOTTOM:
                    return MapObjectMetrics.Y + MapObjectMetrics.Height - MapObject.RECTANGLE_STROKE_THICKNESS;
                default:
                    return CalculateYShifted();
            }
        }

        // vrsi kalkulaciju X koje je pomereno i ogranicavanje da ne izadje iz granica objekta
        private double CalculateXShifted()
        {
            double currentShiftedX = MapObjectMetrics.X + MapObjectMetrics.Width / 2 - GetDoorWidth() / 2 + this.XShift;
            if (currentShiftedX < MapObjectMetrics.X)
                return MapObjectMetrics.X;
            else if (currentShiftedX + GetDoorWidth() > MapObjectMetrics.X + MapObjectMetrics.Width)
                return MapObjectMetrics.X + MapObjectMetrics.Width - GetDoorWidth();
            else return currentShiftedX;
        }

        // vrsi kalkulaciju Y koje je pomereno i ogranicavanje da ne izadje iz granica objekta
        private double CalculateYShifted()
        {
            double currentShiftedY = MapObjectMetrics.Y + MapObjectMetrics.Height / 2 - GetDoorHeight() / 2 + this.YShift;
            if (currentShiftedY < MapObjectMetrics.Y)
                return MapObjectMetrics.Y;
            else if (currentShiftedY + GetDoorHeight() > MapObjectMetrics.Y + MapObjectMetrics.Height)
                return MapObjectMetrics.Y + MapObjectMetrics.Height - GetDoorHeight();
            else return currentShiftedY;
        }

        public Rectangle Rectangle { get => _rectangle; set => _rectangle = value; }
        public MapObjectMetrics MapObjectMetrics { get => _mapObjectMetrics; set => _mapObjectMetrics = value; }
        public MapObjectDoorOrientation MapObjectDoorOrientation { get => _mapObjectDoorOrientation; set => _mapObjectDoorOrientation = value; }
        public double XShift { get => _XShift; set => _XShift = value; }
        public double YShift { get => _YShift; set => _YShift = value; }
    }
}
