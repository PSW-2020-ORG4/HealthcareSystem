using GraphicalEditor.Constants;
using GraphicalEditor.Enumerations;
using Newtonsoft.Json;
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
        [JsonIgnore]
        public Rectangle Rectangle { get; set; }
        public MapObjectMetrics MapObjectMetrics { get; set; }
        public MapObjectDoorOrientation MapObjectDoorOrientation { get; set; }
        public double XShiftFromCenter { get; set; }
        public double YShiftFromCenter { get; set; }

        public MapObjectDoor(MapObjectDoorOrientation mapObjectDoorOrientations, double XShift = 0, double YShift = 0)
        {
            MapObjectDoorOrientation = mapObjectDoorOrientations;
            XShiftFromCenter = XShift;
            YShiftFromCenter = YShift;
        }

        public Rectangle GetDoor()
        {
            if (MapObjectDoorOrientation == MapObjectDoorOrientation.NONE)
                GetEmptyDoor();
            else GetActualDoor();

            return Rectangle;
        }

        private void GetActualDoor()
        {
            Rectangle = new Rectangle();
            Rectangle.Fill = Brushes.DarkSlateGray;
            Rectangle.Width = DoorWidth;
            Rectangle.Height = DoorHeight;

            Rectangle.SetValue(Canvas.LeftProperty, CalculateDoorX());
            Rectangle.SetValue(Canvas.TopProperty, CalculateDoorY());
        }

        private void GetEmptyDoor()
        {
            Rectangle = new Rectangle();
        }

        private double CalculateDoorX()
        {
            switch (MapObjectDoorOrientation)
            {
                case MapObjectDoorOrientation.LEFT:
                    return CalculateXForLeft();
                case MapObjectDoorOrientation.RIGHT:
                    return CalculateXForRight();
                default:
                    return CalculateXShifted();
            }
        }

        private double CalculateXForLeft()
            => MapObjectMetrics.XOfCanvas - DoorWidth + AllConstants.RECTANGLE_STROKE_THICKNESS;

        private double CalculateXForRight()
            => MapObjectMetrics.XOfCanvas + MapObjectMetrics.WidthOfMapObject - AllConstants.RECTANGLE_STROKE_THICKNESS;

        private double CalculateDoorY()
        {
            switch (MapObjectDoorOrientation)
            {
                case MapObjectDoorOrientation.TOP:
                    return CalculateYForTop();
                case MapObjectDoorOrientation.BOTTOM:
                    return CalculateYForBottom();
                default:
                    return CalculateYShifted();
            }
        }        

        private double CalculateYForTop()
            => MapObjectMetrics.YOfCanvas - DoorHeight + AllConstants.RECTANGLE_STROKE_THICKNESS;

        private double CalculateYForBottom()
            => MapObjectMetrics.YOfCanvas + MapObjectMetrics.HeightOfMapObject - AllConstants.RECTANGLE_STROKE_THICKNESS;


        
        private double CalculateXShifted()
        {
            double currentShiftedX = MapObjectMetrics.XOfCanvas + MapObjectMetrics.WidthOfMapObject / 2 - DoorWidth / 2 + this.XShiftFromCenter;
            if (currentShiftedX < MapObjectMetrics.XOfCanvas)
                return MapObjectMetrics.XOfCanvas;
            else if (currentShiftedX + DoorWidth > MapObjectMetrics.XOfCanvas + MapObjectMetrics.WidthOfMapObject)
                return MapObjectMetrics.XOfCanvas + MapObjectMetrics.WidthOfMapObject - DoorWidth;
            else return currentShiftedX;
        }

        
        private double CalculateYShifted()
        {
            double currentShiftedY = MapObjectMetrics.YOfCanvas + MapObjectMetrics.HeightOfMapObject / 2 - DoorHeight / 2 + this.YShiftFromCenter;
            if (currentShiftedY < MapObjectMetrics.YOfCanvas)
                return MapObjectMetrics.YOfCanvas;
            else if (currentShiftedY + DoorHeight > MapObjectMetrics.YOfCanvas + MapObjectMetrics.HeightOfMapObject)
                return MapObjectMetrics.YOfCanvas + MapObjectMetrics.HeightOfMapObject - DoorHeight;
            else return currentShiftedY;
        }

        
        private double DoorWidth
        {
            get
            {
                if (MapObjectDoorOrientation == MapObjectDoorOrientation.TOP || MapObjectDoorOrientation == MapObjectDoorOrientation.BOTTOM)
                    return AllConstants.DOOR_WIDTH;
                else return AllConstants.DOOR_HEIGHT;
            }
        }

        
        private double DoorHeight
        {
            get
            {
                if (MapObjectDoorOrientation == MapObjectDoorOrientation.TOP || MapObjectDoorOrientation == MapObjectDoorOrientation.BOTTOM)
                    return AllConstants.DOOR_HEIGHT;
                else return AllConstants.DOOR_WIDTH;
            }
        }
    }
}
